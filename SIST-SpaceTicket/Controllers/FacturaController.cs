using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Catalogo;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using SIST_SpaceTicket.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class FacturaController : Controller
    {
        private IServiceLugar serviceLugar = new ServiceLugar();
        private IServiceZona serviceZona = new ServiceZona();
        private IServiceEvento serviceEvento = new ServiceEvento();
        private IServiceFactura serviceFactura = new ServiceFactura();
        private IServiceBoleto serviceBoleto = new ServiceBoleto();
        private IServiceFacturaDetalle serviceFacturaDetalle = new ServiceFacturaDetalle();
        // GET: Factura
        public ActionResult Index(string IDEvento)
        {
            Log.Info("Genera detalle factura al cliente " + MethodBase.GetCurrentMethod());
            if (Session["Boletos"] != null && Session["LugaresReservdos"] != null)
            {
                try
                {
                    //creamos el ViewModel que contendra la informacion a mostrar
                    ViewModelDetalleFactura detalleFactura = new ViewModelDetalleFactura();

                    //obtenemos la lista de boletos y cuales lugares fueron reservados 
                    detalleFactura.ListaBoleto = (List<Boleto>)Session["Boletos"];
                    detalleFactura.ListaLugares = (List<Lugar>)Session["LugaresReservdos"];

                    if (detalleFactura.ListaBoleto.Count > 0 && detalleFactura.ListaLugares.Count > 0)
                    {
                        //obtenemos la informacion del lugar dado por el primer boleto
                        Lugar oLugar = serviceLugar.GetLugarByID(detalleFactura.ListaBoleto.First().IDLugar);

                        // obtenemos la zona y evento, dado por la informacion del objeto oLugar => IDZona, IDEvento
                        detalleFactura.Zona = serviceZona.GetZonaByID(oLugar.IDZona);
                        detalleFactura.Evento = serviceEvento.GetEventByID(oLugar.IDEvento);

                        //guardamos en Session el detalle local
                        Session["DetalleFactura"] = detalleFactura;

                        // redireccion al evento
                        return RedirectToAction("Index", "Event", new { IDEvento = detalleFactura.Evento.ID });
                    }
                    else
                    {
                        throw new Exception("Selecciona almenos un campo para continuar con la compra.");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, MethodBase.GetCurrentMethod());
                    TempData["Message"] = ex.Message;
                    TempData["Type"] = "Fail";
                    return RedirectToAction("Index", "Event", new { IDEvento = IDEvento });
                }
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public ActionResult RealizarPago(ViewModelDetalleFactura detalleFactura)
        {
            Log.Info("Realiza pago factura" + MethodBase.GetCurrentMethod());
            //Se obtiene el ID de la tarjeta, puede ser 0 u otro valor
            int IDTarjeta = detalleFactura.IDTarjeta;

            // Se obtiene el detalle de la sesion
            detalleFactura = (ViewModelDetalleFactura)Session["DetalleFactura"];

            // se le asigna a la instancia de detalle en la sesion el ID de tarjeta
            detalleFactura.IDTarjeta = IDTarjeta;
            if (detalleFactura != null)
            {
                try
                {
                    if (ModelState.IsValid && detalleFactura.IDTarjeta != 0)
                    {
                        // Se crea la instanica de factura
                        Factura factura = new Factura()
                        {
                            Cliente = (Cliente)Session["Usuario"],
                            FechaGeneracion = DateTime.Now,
                            IVA = (decimal)detalleFactura.ValorCostoAdministrativos() * detalleFactura.ListaBoleto.Count,
                            SubTotal = (decimal)detalleFactura.ValorBoleto() * detalleFactura.ListaBoleto.Count,
                            Total = (decimal)detalleFactura.TotalCompra(),
                            ListaBoletos = detalleFactura.ListaBoleto,
                            ListaLugar = detalleFactura.ListaLugares,
                            Evento = detalleFactura.Evento,
                        };

                        // Se guarda la factura en la base de datos
                        Factura FacturaGenerada = serviceFactura.Save(factura);

                        // agregamos la variables no mapeadas
                        FacturaGenerada.Cliente = factura.Cliente;
                        FacturaGenerada.Evento = factura.Evento;

                        // Se recorre la lista de boletos y se guardan en la base de datos
                        foreach (var item in detalleFactura.ListaBoleto)
                        {
                            Boleto boleto = serviceBoleto.Save(item);
                            if (boleto == null)
                            {
                                throw new Exception("No se pudo registrar los boletos.");
                            }
                        }

                        // se recorre la lista de Lugares elegidos y se cambia su estado a COMPRADO
                        foreach (var lugar in detalleFactura.ListaLugares)
                        {
                            lugar.Estado = TypeEstadoAsiento.COMPRADO.ToString();
                            Lugar lugarUpdate = serviceLugar.Save(lugar);

                            if (!lugarUpdate.Estado.Equals(TypeEstadoAsiento.COMPRADO.ToString()))
                            {
                                throw new Exception("No se pudo asignar los lugares a comprar.");
                            }
                        }

                        // Se prepara la lista de archivos adjuntos
                        List<Attachment> attachments = new List<Attachment>();

                        // Se recorre la n cantidad de boletos en la lista y se crea
                        // un detalle de factura con la FacturaGenerada despues del insert => Save();
                        for (int i = 0; i < detalleFactura.ListaBoleto.Count; i++)
                        {
                            // creamos la instancia de factura detalle
                            FacturaDetalle oFacturaDetalle = new FacturaDetalle()
                            {
                                IDBoleto = detalleFactura.ListaBoleto[i].ID,
                                IDFactura = FacturaGenerada.ID,
                            };

                            // guardamos el detalle de factura
                            FacturaDetalle detalleGenerado = serviceFacturaDetalle.Save(oFacturaDetalle);
                            if (detalleGenerado == null)
                            {
                                throw new Exception("No se pudo generar el detalle de la factura.");
                            }
                            byte[] PDF = GetBoletoFacturaPDF(FacturaGenerada,
                                                              detalleFactura.ListaLugares[i],
                                                              detalleFactura.ListaBoleto[i]);

                            attachments.Add(new Attachment(new MemoryStream(PDF),
                                                            "Boleto" + detalleFactura.ListaBoleto[i].ID,
                                                            "application/pdf"));
                        }

                        //agregamos la factura como archivo adjunto

                        attachments.Add(new Attachment(
                                        new MemoryStream(GetFacturaPDF(FacturaGenerada)),
                                        "Factura",
                                        "application/pdf"));

                        // ruta XML
                        // string rutaX = @"file:///" + "C:/temp/Factura" + FacturaGenerada.ID + ".xml";

                        // agregamos el XML como adjunto
                        Attachment XML = new Attachment(@"C:\temp\Factura" + FacturaGenerada.ID + ".xml");
                        attachments.Add(XML);

                        IServiceMail serviceMail = new ServiceMail();
                        serviceMail.EnviarFactura(FacturaGenerada.Cliente.Correo, attachments, FacturaGenerada);
                        TempData["PaySuccess"] = "Success";
                        TempData["Message"] = "Reserva exitosa. Se te ha enviado correo con la factura y tus tiquetes. Gracias por tu compra.";
                        Session["DetalleFactura"] = null;
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        TempData["Message"] = "Debe seleccionar una tarjeta para continuar con el pago.";
                        TempData["Type"] = "Fail";
                        return RedirectToAction("Index", new { IDEvento = detalleFactura.Evento.ID });
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, MethodBase.GetCurrentMethod());
                    TempData["Message"] = ex.Message;
                    TempData["Type"] = "Fail";
                    return RedirectToAction("Index", "Event", new { IDEvento = detalleFactura.Evento.ID });
                }
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        public byte[] GetFacturaPDF(Factura oFactura)
        {
            Log.Info("Genera factura"+ oFactura.ID + MethodBase.GetCurrentMethod());
            string rptPath = "";
            Microsoft.Reporting.WebForms.ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            string deviceInfo = "";
            try
            {
                // Leer parámetro

                // Generar QR
                // Valida si el directorio existe, sino lo crea
                if (!Directory.Exists(@"C:\temp\"))
                    Directory.CreateDirectory(@"C:\temp\");

                //convertir imagen del evento 
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(oFactura.Evento.Imagen);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            // If you want it as Png
                            yourImage.Save(@"c:\temp\event.png", ImageFormat.Png);


                        }
                    }

                }
                // Convertir imagen a QR
                Image imagen = QuickResponse.QuickResponseGenerador(oFactura.ID, 53);
                //Image imagen = QuickResponse.QuickResponseGenerador(txtFiltro, 53);
                // Salvar imagen
                imagen.Save(@"c:\temp\code.png", ImageFormat.Png);

                // Declarar Fuente
                Reportes.DSFuenteDatos dSFuenteDatos = new Reportes.DSFuenteDatos();

                // Instancia el Table Adaptar ojo como se debe acceder desde el NameSpace
                SIST_SpaceTicket.Reportes.DSFuenteDatosTableAdapters.FacturaTableAdapter factura = new Reportes.DSFuenteDatosTableAdapters.FacturaTableAdapter();
                // Llena la tabla            
                factura.Fill(dSFuenteDatos.Factura, oFactura.ID);
                // Colocar la ruta del reporte
                rptPath = Server.MapPath("~/Reportes/rptFactura.rdlc");

                // Cargar la lista con la información
                var lista = dSFuenteDatos.Factura.ToList();

                // Primer parametro NOMBRE del DataSet que se le coloco en el REPORT
                Microsoft.Reporting.WebForms.ReportDataSource rds
                    = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", lista);

                rv.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                rv.LocalReport.ReportPath = rptPath;
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.EnableHyperlinks = true;

                // Parametros del valor en colones y dolares del monto total
                String montoDolares = Util.MonedaEnLetras.Convertir
                                    (oFactura.Total.ToString(), true, "dólares", "centavos") + " (USD)";
                String montoColones;

                // dólares a colones
                decimal colones = oFactura.Total * (decimal)new ServiceBCCR().GetDolar("c").Monto;
                montoColones = Util.MonedaEnLetras.Convertir
                                    (colones.ToString(), true, "colones", "céntimos") + " (CRC)";

                // Parametros del QR
                // Config imagen del QR
                string ruta1 = @"file:///" + @"C:/TEMP/code.png";
                string ruta2 = @"file:///" + @"C:/TEMP/event.png";
                // Habilitar imagenes externas
                rv.LocalReport.EnableExternalImages = true;
                // Pasar parámetro
                List<ReportParameter> listaParametros = new List<ReportParameter>();
                listaParametros.Add(new ReportParameter("quickresponse", ruta1));
                listaParametros.Add(new ReportParameter("event", ruta2));
                listaParametros.Add(new ReportParameter("moneydollar", montoDolares));
                listaParametros.Add(new ReportParameter("moneycolon", montoColones));
                rv.LocalReport.SetParameters(listaParametros);

                rv.LocalReport.Refresh();
                deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "  <EmbedFonts>None</EmbedFonts>" +
                "</DeviceInfo>";

                streamBytes = rv.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                System.IO.File.WriteAllBytes(@"c:\temp\producto.pdf", streamBytes);


                // Retorna un pdf
                return streamBytes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Type"] = "Fail";
                throw;
            }
        }

        public byte[] GetBoletoFacturaPDF(Factura oFactura, Lugar oLugar, Boleto oBoleto)
        {
            string rptPath = "";
            Microsoft.Reporting.WebForms.ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;
            string deviceInfo = "";
            try
            {
                // Leer parámetro

                // Generar QR
                // Valida si el directorio existe, sino lo crea
                if (!Directory.Exists(@"C:\temp\"))
                    Directory.CreateDirectory(@"C:\temp\");

                //convertir imagen del evento 
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(oFactura.Evento.Imagen);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            // If you want it as Png
                            yourImage.Save(@"c:\temp\event.png", ImageFormat.Png);


                        }
                    }

                }
                // Convertir imagen a QR
                Image imagen = QuickResponse.QuickResponseGenerador(oBoleto.ID, 53);
                //Image imagen = QuickResponse.QuickResponseGenerador(txtFiltro, 53);
                // Salvar imagen
                imagen.Save(@"c:\temp\code.png", ImageFormat.Png);

                // Declarar Fuente
                Reportes.DSFuenteDatos dSFuenteDatos = new Reportes.DSFuenteDatos();

                // Instancia el Table Adaptar ojo como se debe acceder desde el NameSpace
                SIST_SpaceTicket.Reportes.DSFuenteDatosTableAdapters.BoletoTableAdapter boleto = new Reportes.DSFuenteDatosTableAdapters.BoletoTableAdapter();
                // Llena la tabla            
                boleto.Fill(dSFuenteDatos.Boleto, oLugar.ID);
                // Colocar la ruta del reporte
                rptPath = Server.MapPath("~/Reportes/rptBoleto.rdlc");

                // Cargar la lista con la información
                var lista = dSFuenteDatos.Boleto.ToList();

                // Primer parametro NOMBRE del DataSet que se le coloco en el REPORT
                Microsoft.Reporting.WebForms.ReportDataSource rds
                    = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet", lista);

                rv.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                rv.LocalReport.ReportPath = rptPath;
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.EnableHyperlinks = true;

                // Parametros del QR
                // Config imagen del QR
                string ruta1 = @"file:///" + @"C:/TEMP/code.png";
                string ruta2 = @"file:///" + @"C:/TEMP/event.png";
                // Habilitar imagenes externas
                rv.LocalReport.EnableExternalImages = true;
                // Pasar parámetro
                List<ReportParameter> listaParametros = new List<ReportParameter>();
                listaParametros.Add(new ReportParameter("quickresponse", ruta1));
                listaParametros.Add(new ReportParameter("event", ruta2));
                rv.LocalReport.SetParameters(listaParametros);

                rv.LocalReport.Refresh();
                deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "  <EmbedFonts>None</EmbedFonts>" +
                "</DeviceInfo>";

                streamBytes = rv.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                System.IO.File.WriteAllBytes(@"c:\temp\Boleto-" + oBoleto.ID + ".pdf", streamBytes);
                // Retorna un pdf
                return streamBytes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Type"] = "Fail";
                throw;
            }
        }

        // ******************************************************************
        // ***********             CONSULTA DE FACTURA          *************
        // ******************************************************************

        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]

        public ActionResult ConsultaFactura()
        {
            Log.Info("Se ingresa a: " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult Get(DataSourceLoadOptions loadOptions)
        {
            List<ViewModelFactura> listaViewModel = new List<ViewModelFactura>();
            List<Factura> listaFacturas = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                listaFacturas = serviceFactura.GetFactura().ToList();

                foreach (Factura factura in listaFacturas)
                {
                    // se obtiene el detalle de cada factura
                    List<FacturaDetalle> listaDetalle = serviceFacturaDetalle.GetAllFacturaDetalleByFactura(factura.ID).ToList();

                    // con el primer elemento de la lista se obtiene el boleto que contiene el Asiento o Lugar
                    Boleto boleto = serviceBoleto.GetBoletoByID(listaDetalle.First().IDBoleto);

                    // El Lugar o Asiento contiene el ID del Evento y con eso traemos el objeto Evento
                    Evento evento = serviceEvento.GetEventByID(serviceLugar.GetLugarByID(boleto.IDLugar).IDEvento);

                    // El Lugar o Asiento contiene el ID de la Zona y con eso traemos el objeto Zona
                    Zona zona = serviceZona.GetZonaByID(serviceLugar.GetLugarByID(boleto.IDLugar).IDZona);

                    ViewModelFactura viewModelFactura = new ViewModelFactura()
                    {
                        IDFactura = factura.ID,
                        EventoName = evento.Descripcion,
                        ZonaName = zona.Descripcion,
                        FechaPago = factura.FechaGeneracion,
                        Impuesto = factura.IVA,
                        SubTotal = factura.SubTotal,
                        Total = factura.Total
                    };

                    listaViewModel.Add(viewModelFactura);
                }

                if (listaViewModel != null && listaViewModel.Count != 0)
                {
                    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(listaViewModel, loadOptions)), "application/json");
                }
                else
                {
                    throw new Exception("No hay información");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        public ActionResult GetDetalleBoletoFactua(String IDFactura, DataSourceLoadOptions loadOptions)
        {
            List<ViewModelBoletoDetalle> listaViewModel = new List<ViewModelBoletoDetalle>();
            try
            {
                loadOptions.SortByPrimaryKey = false;

                // se obtiene el detalle de la factura
                List<FacturaDetalle> listaDetalle = serviceFacturaDetalle.GetAllFacturaDetalleByFactura(IDFactura).ToList();

                foreach(FacturaDetalle detalle in listaDetalle)
                {
                    // Se obtiene el boleto con el ID del detalle
                    Boleto boleto = serviceBoleto.GetBoletoByID(detalle.IDBoleto);

                    // Se obtiene el Lugar del detalle con el boleto obtenido
                    Lugar lugar = serviceLugar.GetLugarByID(boleto.IDLugar);

                    // El Lugar o Asiento contiene el ID de la Zona y con eso traemos el objeto Zona
                    Zona zona = serviceZona.GetZonaByID(lugar.IDZona);


                    ViewModelBoletoDetalle boletoDetalle = new ViewModelBoletoDetalle()
                    {
                        IDBoleto = boleto.ID,
                        PrecioBoleto = zona.Precio,
                        Asiento = lugar.NumeroAsiento,
                        Fila = lugar.Fila,
                    };

                    listaViewModel.Add(boletoDetalle);
                }


                if (listaViewModel != null || listaViewModel.Count != 0)
                {
                    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(listaViewModel, loadOptions)), "application/json");
                }
                else
                {
                    throw new Exception("No hay información");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);

            }
        }
    }
}
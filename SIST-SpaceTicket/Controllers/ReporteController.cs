using ApplicationCore.Services;
using Infraestructure.Models.Catalogo;
using Microsoft.Reporting.WebForms;
using SIST_SpaceTicket.Reportes;
using SIST_SpaceTicket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class ReporteController : Controller
    {
        private IServiceBoleto serviceBoleto = new ServiceBoleto();

        // GET: Reporte
        [Authorize(Roles = "ADMINISTRADOR, REPORTEADOR")]
        public ActionResult Index()
        {
            Log.Info("Ejecuta controlador Reporte: " + MethodBase.GetCurrentMethod());
            return View();
        }

        [Authorize(Roles = "ADMINISTRADOR, REPORTEADOR")]
        public ActionResult ReporteVentasDiarias()
        {
            Log.Info("Se visita: " + MethodBase.GetCurrentMethod());
            return View("ReporteVentasDiarias");
        }


        [HttpPost]
        public ActionResult ReporteVentasDiarias(FormCollection form)
        {
            Log.Info("Ejecuta controlador Reporte: " + MethodBase.GetCurrentMethod());
            string rptPath = "";
            ReportViewer rv = new ReportViewer();
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;
            string deviceInfo = "";
            string dtpFiltroInicio = "";
            string dtpFiltroFin = "";

            try
            {
                // Leer parámetros
                dtpFiltroInicio = form["dtpInicio"].ToString();
                dtpFiltroFin = form["dtpFin"].ToString();


                if (string.IsNullOrEmpty(dtpFiltroInicio) || string.IsNullOrEmpty(dtpFiltroFin))
                {
                    throw new Exception("Filtro de la fecha de inicio y de fin es un dato requerido");
                }

                DateTime StartDate = DateTime.Parse(dtpFiltroInicio + " 00:00:00");
                DateTime EndDate = DateTime.Parse(dtpFiltroFin + " 23:59:59");
                // Declarar Fuente
                DSFuenteDatos dSFuenteDatos = new DSFuenteDatos();

                // Instancia el Table Adaptar ojo como se debe acceder desde el NameSpace
                SIST_SpaceTicket.Reportes.DSFuenteDatosTableAdapters.VentasDiariasTableAdapter ventasDiariasTableAdapter = new Reportes.DSFuenteDatosTableAdapters.VentasDiariasTableAdapter();
                // Llena la tabla

                ventasDiariasTableAdapter.Fill(dSFuenteDatos.VentasDiarias, StartDate, EndDate);
                // Colocar la ruta del reporte
                rptPath = Server.MapPath("~/Reportes/rptVentasDiarias.rdlc");

                var lista = dSFuenteDatos.VentasDiarias.ToList();

                // Primer parametro NOMBRE del DataSet que se le coloco en el REPORT
                ReportDataSource rds = new ReportDataSource("DataSet", lista);

                //****************************************************************
                // Debe tener exactamente el mismo ORDEN de acá en adelante.
                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = rptPath;
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.EnableHyperlinks = true;

                // Habilitar imagenes externas
                rv.LocalReport.EnableExternalImages = true;
                // Pasar parámetro
                List<ReportParameter> listaParametros = new List<ReportParameter>();
                listaParametros.Add(new ReportParameter("inicio", StartDate.ToShortDateString()));
                listaParametros.Add(new ReportParameter("fin", EndDate.ToShortDateString()));
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

                // Se guarda en una variable de sesión el reporte para pasarlo a la vista parcial
                Session["ReporteVentasDiarias"] = streamBytes;

                // se obtiene el detalle de boletos para el grafico
                List<Boleto> listaBoleto = serviceBoleto.GetBoleto().
                    Where(x => x.FechaPago >= StartDate
                                && x.FechaPago <= EndDate).
                    ToList();

                // lista auxiliar para la cantidad de boletos
                List<Boleto> aux;

                // detalle grafico
                List<ViewModelReporteVentasDiarias> detalleGrafico = new List<ViewModelReporteVentasDiarias>();

                // cantidad de veces que se recorrera la lista
                int diferenciaDia = 1;
                if (!dtpFiltroInicio.Equals(dtpFiltroFin))
                {
                    // se le suma 1 por que no cuenta el ultimo dia entero
                    // ejemplo del dia 10 al 14 hay 4 dias de diferencia
                    // pero en la consulta tambien se quiere saber si hay compas durante todo el dia "14"
                    // es decir el dia 14 hasta las 23:59:59 horas.
                    diferenciaDia = (EndDate - StartDate).Days + 1; 
                }
                DateTime FechaCreciente = StartDate;
                for (int i = 1; i <= diferenciaDia; i++)
                {
                    aux = listaBoleto.
                        Where(x => x.FechaPago.ToShortDateString().Equals(FechaCreciente.ToShortDateString())).
                        ToList();
                    detalleGrafico.Add(new ViewModelReporteVentasDiarias()
                    {
                        Cantidad = aux.Count,
                        FechaPago = FechaCreciente
                    });
                    FechaCreciente = FechaCreciente.AddDays(1);
                }

                // se guarda en TempData el ViewModelReporteVentasDiarias
                TempData["BoletosGrafico"] = detalleGrafico;
                return View("ReporteVentasDiarias");
                // Retorna un pdf
                //return File(streamBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Type"] = "Fail";
                return View("ReporteVentasDiarias");
            }

        }

        public ActionResult GeneraReporteVentaDiarias()
        {
            Log.Info("Ejecuta controlador Reporte: " + MethodBase.GetCurrentMethod());
            if (Session["ReporteVentasDiarias"] != null)
            {
                byte[] streamBytes = (byte[])Session["ReporteVentasDiarias"];
                Session["ReporteVentasDiarias"] = null;
                return File(streamBytes, "application/pdf");
            }
            else
            {
                TempData["Message"] = "Fallo al generar el PDF";
                TempData["Type"] = "Fail";
                return View("ReporteVentasDiarias");
            }
        }

        [Authorize(Roles = "ADMINISTRADOR, REPORTEADOR")]
        public ActionResult ReporteEventoAsientos()
        {
            Log.Info("Se visita: " + MethodBase.GetCurrentMethod());
            return View("ReporteEventoAsientos");
        }

        [HttpPost]
        public ActionResult ReporteEventoAsientos(FormCollection form)
        {
            Log.Info("Ejecuta controlador Reporte: " + MethodBase.GetCurrentMethod());
            string rptPath = "";
            ReportViewer rv = new ReportViewer();
            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;
            string deviceInfo = "";
            string dtpFiltroInicio = "";
            string dtpFiltroFin = "";
            string IDEvento = "";
            try
            {
                // Leer parámetros
                dtpFiltroInicio = form["dtpInicio"].ToString();
                dtpFiltroFin = form["dtpFin"].ToString();
                IDEvento = form["IDEvento"].ToString();

                if (string.IsNullOrEmpty(dtpFiltroInicio) || string.IsNullOrEmpty(dtpFiltroFin)
                    || string.IsNullOrEmpty(IDEvento))
                {
                    throw new Exception("Filtro del evento, la fecha de inicio y de fin es un dato requerido.");
                }

                // variables locales con la fecha de inicio en valor minimo
                // y fecha final con el valor de tiempo máximo
                DateTime StartDate = DateTime.Parse(dtpFiltroInicio + " 00:00:00");
                DateTime EndDate = DateTime.Parse(dtpFiltroFin + " 23:59:59");
                // Declarar Fuente
                DSFuenteDatos dSFuenteDatos = new DSFuenteDatos();

                // Instancia el Table Adaptar ojo como se debe acceder desde el NameSpace
                SIST_SpaceTicket.Reportes.DSFuenteDatosTableAdapters.EventoAsientosTableAdapter eventoAsientoTableAdapter = new Reportes.DSFuenteDatosTableAdapters.EventoAsientosTableAdapter();
                // Llena la tabla

                eventoAsientoTableAdapter.Fill(dSFuenteDatos.EventoAsientos, IDEvento, StartDate, EndDate);
                // Colocar la ruta del reporte
                rptPath = Server.MapPath("~/Reportes/rptEventoAsientos.rdlc");

                var lista = dSFuenteDatos.EventoAsientos.ToList();

                // Primer parametro NOMBRE del DataSet que se le coloco en el REPORT
                ReportDataSource rds = new ReportDataSource("DataSet", lista);

                //****************************************************************
                // Debe tener exactamente el mismo ORDEN de acá en adelante.
                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = rptPath;
                rv.LocalReport.DataSources.Add(rds);
                rv.LocalReport.EnableHyperlinks = true;

                // Habilitar imagenes externas
                rv.LocalReport.EnableExternalImages = true;
                // Pasar parámetro
                List<ReportParameter> listaParametros = new List<ReportParameter>();
                listaParametros.Add(new ReportParameter("inicio", StartDate.ToShortDateString()));
                listaParametros.Add(new ReportParameter("fin", EndDate.ToShortDateString()));
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


                // Retorna un pdf
                return File(streamBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Type"] = "Fail";
                return View("ReporteEventoAsientos");
            }

        }
    }
}
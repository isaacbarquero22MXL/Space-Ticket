using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ApplicationCore.Services
{
    public class ServiceFactura : IServiceFactura
    {
        private IRepositoryFactura _RepositoryFactura = new RepositoryFactura();
        private IRepositoryLugar _RepositoryLugar = new RepositoryLugar();
        private IRepositoryZona _RepositoryZona = new RepositoryZona();
        private IRepositoryBCCR _RepositoryBCCR = new RepositoryBCCR();

        private static string IDConsecutivo = "F0010000101000000000001";
        private static int Consecutivo = 0;
        private static string IDFactura;
        public void DeleteFactura(string id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(string id)
        {
            return _RepositoryFactura.Existe(id);
        }

        public string GenerateFacturaID()
        {
            GenerateID(); // se genera el ID de la factura y se almacena en la variable privada
            return IDFactura;
        }

        public void GenerateID()
        {
            String ID = IDConsecutivo + ++Consecutivo;
            IDFactura = ID;
            if (Existe(ID))
                GenerateID();
        }

        public IEnumerable<Factura> GetFactura()
        {
           return _RepositoryFactura.GetFactura();
        }

        public Factura GetFacturaByID(string id)
        {
            return _RepositoryFactura.GetFacturaByID(id);
        }

        public Factura Save(Factura Factura)
        {
            Factura.ID = GenerateFacturaID();
            Factura.FacturaXML = ObtenerXML(Factura);
           return _RepositoryFactura.Save(Factura);
        }

        public string GenerarClaveNumerica(String IDFactura)
        {
            StringBuilder h = new StringBuilder();
            h.Append("506"); //codigo pais CR

            DateTime fecha = DateTime.Now;
            int anno = fecha.Year % 100; // obtenemos el año en dos digitos
            int mes = fecha.Month; //obtenemos el mes en dos digitos
            int dia = fecha.Day;// obtenemos el día 

            if (dia < 10)
            {
                h.Append("0").Append(dia);// si es menor que diez agregamos 0  a la combinacion de digitos
            }
            else
            {
                h.Append(dia);
            }
            h.Append(mes);
            h.Append(anno);
            h.Append("310262039"); // cedula juridica
            h.Append(IDFactura); // ID Consecutivo
            h.Append("1"); // Tipo Normal
            h.Append("20391026"); //Clave seguridad propietario

            return h.ToString();
        }

        public string ObtenerXML(Factura oFactura)
        {
            XmlDocument documento = new XmlDocument();
            XmlDeclaration dec = documento.CreateXmlDeclaration("1.0", null, null);
            documento.AppendChild(dec);

            XmlElement FacturaElectronica = documento.CreateElement("FacturaElectronica",
                "https://www.hacienda.go.cr/ATV/ComprobanteElectronico/docs/esquemas/2016/v4.3/FacturaElectronica_V4.3.xsd");

            //Generar encabezado
            XmlElement Encabezado = documento.CreateElement("Encabezado");

            //Agregamos datos de la empresa Space Ticket
            XmlElement EmpresaServicio = documento.CreateElement("EmpresaServicio");

            XmlElement NombreEmpresa = documento.CreateElement("NombreEmpresa");
            NombreEmpresa.InnerText = "Space Ticket";
            EmpresaServicio.AppendChild(NombreEmpresa);


            XmlElement IdentificacionJuridica = documento.CreateElement("IdentificacionJuridica");
            IdentificacionJuridica.InnerText = "3-1026-2039";
            EmpresaServicio.AppendChild(IdentificacionJuridica);

            Encabezado.AppendChild(EmpresaServicio);

            //Iniciamos con los datos del encabezado
            XmlElement TipoFactura = documento.CreateElement("NombreFactura");
            TipoFactura.InnerText = "Factura Electrónica";
            Encabezado.AppendChild(TipoFactura);

            XmlElement ClaveNumerica = documento.CreateElement("ClaveNumerica");
            ClaveNumerica.InnerText = GenerarClaveNumerica(oFactura.ID);
            Encabezado.AppendChild(ClaveNumerica);

            XmlElement NumeroConsecutivo = documento.CreateElement("NumeroConsecutivo");

            // Se quita el literal "F" del la factura para agregar el consecutivo
            NumeroConsecutivo.InnerText = oFactura.ID.Substring(1, oFactura.ID.Length - 1);
            Encabezado.AppendChild(NumeroConsecutivo);

            XmlElement FechaEmision = documento.CreateElement("FechaEmision");
            FechaEmision.InnerText = DateTime.Now.ToShortDateString();
            Encabezado.AppendChild(FechaEmision);

            XmlElement CondicionVenta = documento.CreateElement("CondicionVenta");
            CondicionVenta.InnerText = "Crédito";
            Encabezado.AppendChild(CondicionVenta);

            XmlElement TipoPago = documento.CreateElement("TipoPago");
            TipoPago.InnerText = "Tarjeta";
            Encabezado.AppendChild(TipoPago);

            //Creamos el emisor Negocio

            XmlElement Emisor = documento.CreateElement("Emisor");

            XmlElement NombreN = documento.CreateElement("Nombre");
            NombreN.InnerText = "Space Ticket";
            Emisor.AppendChild(NombreN);

            XmlElement CedulaJuridica = documento.CreateElement("CedulaJuridica");
            XmlElement NumeroCJ = documento.CreateElement("Numero");
            NumeroCJ.InnerText = "3-1026-2039";
            CedulaJuridica.AppendChild(NumeroCJ);
            Emisor.AppendChild(CedulaJuridica);

            XmlElement CorreoN = documento.CreateElement("CorreoElectronico");
            CorreoN.InnerText = "SpaceTicket@gmail.com";

            Emisor.AppendChild(CorreoN);
            Encabezado.AppendChild(Emisor);


            // Creamos el receptor
            XmlElement Receptor = documento.CreateElement("Receptor");

            XmlElement NombreC = documento.CreateElement("Nombre");
            NombreC.InnerText = oFactura.Cliente.Nombre;
            Receptor.AppendChild(NombreC);

            XmlElement Identificacion = documento.CreateElement("Identificacion");
            XmlElement NumeroI = documento.CreateElement("Numero");
            NumeroI.InnerText = oFactura.Cliente.Cedula;
            Identificacion.AppendChild(NumeroI);
            Receptor.AppendChild(Identificacion);

            XmlElement UbicacionC = documento.CreateElement("Ubicacion");
            XmlElement DireccionC = documento.CreateElement("Nacionalidad");
            DireccionC.InnerText = oFactura.Cliente.Nacionalidad;
            UbicacionC.AppendChild(DireccionC);

            Receptor.AppendChild(UbicacionC);

            XmlElement CorreoC = documento.CreateElement("CorreoElectronico");
            CorreoC.InnerText = oFactura.Cliente.Correo;

            Receptor.AppendChild(CorreoC);

            Encabezado.AppendChild(Receptor);

            //cierra el encabezado
            FacturaElectronica.AppendChild(Encabezado);

            //generamos detalleFactura
            XmlElement DetalleServicio = documento.CreateElement("DetalleServicio");
            XmlElement factura = documento.CreateElement("factura");

            //detalle producto
            XmlElement detalleProducto = documento.CreateElement("detalle");

            foreach (var item in oFactura.ListaBoletos)
            {
                XmlElement Boleto = documento.CreateElement("Boleto");
                XmlElement codigoBoleto = documento.CreateElement("codigoBoleto");
                codigoBoleto.InnerText = item.ID;

                XmlElement fechaBoleto = documento.CreateElement("fechaBoleto");
                fechaBoleto.InnerText = item.FechaPago.ToShortDateString();

                XmlElement precioUnitario = documento.CreateElement("precioUnitario");
                precioUnitario.InnerText = _RepositoryZona.GetZonaByID
                    (_RepositoryLugar.GetLugarByID(item.IDLugar).IDZona).Precio.ToString();

                XmlElement cantidadUnidad = documento.CreateElement("cantidadUnidad");
                cantidadUnidad.InnerText = "1";

                Boleto.AppendChild(codigoBoleto);
                Boleto.AppendChild(fechaBoleto);
                Boleto.AppendChild(precioUnitario);
                Boleto.AppendChild(cantidadUnidad);

                detalleProducto.AppendChild(Boleto);
            }

            //detalle lugares
            XmlElement detalleLugares = documento.CreateElement("detalleLugares");

            foreach (var lugar in oFactura.ListaLugar)
            {
                XmlElement Lugar = documento.CreateElement("Lugar");

                XmlElement codigoLugar = documento.CreateElement("codigoLugar");
                codigoLugar.InnerText = lugar.ID.ToString();

                XmlElement lugarFila = documento.CreateElement("lugarFila");
                lugarFila.InnerText = lugar.Fila;

                XmlElement lugarAsiento = documento.CreateElement("lugarAsiento");
                lugarAsiento.InnerText = lugar.NumeroAsiento;


                Lugar.AppendChild(codigoLugar);
                Lugar.AppendChild(lugarFila);
                Lugar.AppendChild(lugarAsiento);
               

                detalleLugares.AppendChild(Lugar);
            }
            factura.AppendChild(detalleProducto);
            factura.AppendChild(detalleLugares);
            //cerramos los detalles

            //registramos los totales, impuesto, y añadidos
            XmlElement CodigoTipoMoneda = documento.CreateElement("CodigoTipoMoneda");

            XmlElement colones = documento.CreateElement("CodigoMoneda");
            colones.InnerText = "CRC";
            CodigoTipoMoneda.AppendChild(colones);

            XmlElement DolarCambio = documento.CreateElement("TipoCambio");
            DolarCambio.InnerText = _RepositoryBCCR.GetDolar("c").Monto.ToString("N2");
            CodigoTipoMoneda.AppendChild(DolarCambio);

            XmlElement dolares = documento.CreateElement("CodigoMoneda");
            dolares.InnerText = "USD";
            CodigoTipoMoneda.AppendChild(dolares);

            XmlElement ColonCambio = documento.CreateElement("TipoCambio");
            ColonCambio.InnerText = _RepositoryBCCR.GetValueDollarToColones().ToString("N6");
            CodigoTipoMoneda.AppendChild(ColonCambio);

            factura.AppendChild(CodigoTipoMoneda);

            XmlElement costoServicio = documento.CreateElement("CostoServicio");
            costoServicio.InnerText = "900" + " colones";
            factura.AppendChild(costoServicio);

            XmlElement valorImpuesto = documento.CreateElement("ValorImpuesto");
            valorImpuesto.InnerText = "Igual al 2%";
            factura.AppendChild(valorImpuesto);

            XmlElement subTotal = documento.CreateElement("SubTotal");
            subTotal.InnerText = oFactura.SubTotal.ToString("N2");
            factura.AppendChild(subTotal);

            XmlElement ImpuestoCompra = documento.CreateElement("ImpuestoCompra");
            ImpuestoCompra.InnerText = oFactura.IVA.ToString() + ".00";
            factura.AppendChild(ImpuestoCompra);

            XmlElement TotalNetoCompra = documento.CreateElement("TotalNetoCompra");
            TotalNetoCompra.InnerText = oFactura.Total.ToString("N2");
            factura.AppendChild(TotalNetoCompra);

            DetalleServicio.AppendChild(factura);
            FacturaElectronica.AppendChild(DetalleServicio);
            documento.AppendChild(FacturaElectronica);

            documento.Save("C:/temp/Factura"+oFactura.ID+".xml");

            return documento.InnerXml;
        }
    }
}

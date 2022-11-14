using Infraestructure.Models.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryBCCR : IRepositoryBCCR
    {
        private readonly string TOKEN = "23I25SN80R";
        private readonly string NOMBRE = "Isaac Barquero Lizano";
        private readonly string CORREO = "isaacbarquero22@gmail.com";

        public Dolar GetDolar(String pCompraoVenta)
        {

            try
            {
                Dolar dolar = null;
                DataSet dataset = null;
                string tipoCompraoVenta;

                // se compara si es compra o venta 318 o 317
                tipoCompraoVenta = pCompraoVenta.Equals("c", StringComparison.InvariantCulture) ? "317" : "318";

                // Protocolo de comunicaciones
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                // Se instancia al Servicio Web
                BCCR.wsindicadoreseconomicosSoapClient client =
                    new BCCR.wsindicadoreseconomicosSoapClient("wsindicadoreseconomicosSoap12");

                // Se invoca.
                dataset = client.ObtenerIndicadoresEconomicos(tipoCompraoVenta, DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), NOMBRE, "N", CORREO, TOKEN);

                DataTable table = dataset.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    // Validar el error. No es la forma correcta pero bueno.
                    if (row[0].ToString().Contains("error"))
                    {
                        throw new Exception(row[0].ToString());
                    }
                    dolar = new Dolar();
                    dolar.Codigo = row[0].ToString();
                    dolar.Fecha = DateTime.Parse(row[1].ToString());
                    dolar.Monto = Convert.ToDouble(row[2].ToString());
                    break;
                }

                return dolar;
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                throw;
            }
        }

        public double GetValueDollarToColones()
        {
            Dolar oDolar = GetDolar("c");
            return 1 / oDolar.Monto;
        }
    }
}

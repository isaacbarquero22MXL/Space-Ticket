using ApplicationCore.Services;
using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class BoletoController : Controller
    {
        IServiceBoleto _ServiceBoleto = new ServiceBoleto();
        IServiceLugar _ServiceLugar= new ServiceLugar();
        // GET: Boleto
        public ActionResult Index()
        {
            Log.Info("Visita Boleto: Método" + MethodBase.GetCurrentMethod());
            return View();
        }

        [HttpPost]
        public void Book(int lugar)
        {
            Log.Info("Visita" + MethodBase.GetCurrentMethod());
            List<Boleto> listaBoletos = (List<Boleto>)Session["Boletos"];
            List<Lugar> listaLugares = (List<Lugar>)Session["LugaresReservdos"];
            Cliente cliente = (Cliente)Session["Usuario"];

            Boleto isBoletoInList = listaBoletos.Where(x => x.IDLugar == lugar).FirstOrDefault();
            Lugar oLugar = _ServiceLugar.GetLugarByID(lugar);

            if (isBoletoInList == null)
            {                
                Boleto boleto = new Boleto()
                {
                    ID = _ServiceBoleto.GenerateBoletoID(),
                    IDCliente = cliente.CodigoCliente,
                    CedulaCliente = cliente.Cedula,
                    FechaPago = DateTime.Now,
                    IDLugar = lugar
                };
                listaBoletos.Add(boleto);
                listaLugares.Add(oLugar);
            }
            else
            {
                listaBoletos.Remove(isBoletoInList);
                listaLugares = listaLugares.AsEnumerable().Where(x => x.ID != oLugar.ID).ToList();
            }

            Session["Boletos"] = listaBoletos;
            Session["LugaresReservdos"] = listaLugares;
        }
    }
}
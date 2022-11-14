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
    public class LugarController : Controller
    {
        private IServiceLugar serviceLugar = new ServiceLugar();
        private IServiceZona serviceZona= new ServiceZona();
        private IServiceEvento serviceEvento = new ServiceEvento();
        // GET: Lugar
        public ActionResult Index()
        {
            Log.Info("Visita controlador Lugar : " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult FindSeats(string IDEvento, int IDZona)
        {
            Log.Info("Se buscó los asientos para el evento con ID: " + IDEvento + " y la zona con el ID: " + IDZona);
            try
            {
                IEnumerable<Lugar> lista = serviceLugar.GetLugarByEventZone(IDEvento, IDZona);

                IEnumerable<Zona> listaZona= serviceZona.GetZonasByIDLugarEvento(serviceEvento.GetEventByID(IDEvento).LugarEvento);
                TempData["ZonasEvento"] = listaZona;
                TempData["IDEventoTemp"] = IDEvento;
                if (lista == null || lista.Count() <= 0)
                {
                    throw new Exception("No hay informacion de asientos.");
                }
                TempData["ZonaTemporal"] = serviceZona.GetZonaByID(IDZona);
                TempData["EventZoneSeats"] = lista;
                Session["Boletos"] = new List<Boleto>();
                Session["LugaresReservdos"] = new List<Lugar>();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Type"] = "Fail"; ;
            }

            return RedirectToAction("Index", "Event", new {IDEvento = IDEvento});
        }
    }
}
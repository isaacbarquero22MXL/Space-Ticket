using ApplicationCore.Services;
using Infraestructure.Models.Catalogo;
using Infraestructure.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class HomeController : Controller
    {
        private IServiceEvento serviceEvento = new ServiceEvento();
        public ActionResult Index()
        {
            Log.Info("Visita: " + MethodBase.GetCurrentMethod());
            IEnumerable<Evento> listaEventos = serviceEvento.GetAllEvents();
            listaEventos = listaEventos.Where(x => !x.Estado.Equals(TypeEstadoEvento.CANCELADO.ToString())
                                                && x.Fecha >= DateTime.Now);
            if(listaEventos.Count() > 0)
            {
                Evento mainEvent = listaEventos.First(); // añade el primer evento
                ViewBag.MainEvent = mainEvent;
                ViewBag.Eventos = listaEventos.Where(e => e.ID != mainEvent.ID); // guarda la lista sin el main event
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            Log.Info("Visita: " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            Log.Info("Visita: " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult AutoEvaluacion()
        {
            Log.Info("Visita: " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult CldrData()
        {
            return new DevExtreme.AspNet.Mvc.CldrDataScriptBuilder()
                .SetCldrPath("~/Content/cldr-data")
                .SetInitialLocale("es")
                .UseLocales("es", "es")
                .Build();
        }
    }
}
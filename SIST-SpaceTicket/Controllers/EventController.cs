using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Catalogo;
using Newtonsoft.Json;
using SIST_SpaceTicket.Util;
using SIST_SpaceTicket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class EventController : Controller
    {
        private IServiceEvento serviceEvento = new ServiceEvento();
        private IServiceZona serviceZona= new ServiceZona();
        private IServiceLugar serviceLugar= new ServiceLugar();
        // GET: Event
        public ActionResult Index(String IDEvento)
        {
            Log.Info("Visita" + MethodBase.GetCurrentMethod());
            Evento evento = serviceEvento.GetEventByID(IDEvento);
            if (evento != null)
            {
                ViewModelEvento viewModelEvento = new ViewModelEvento();
                viewModelEvento.Evento = evento;
                viewModelEvento.Month = MesALetra.GetMesAbreviadoByNumber(evento.Fecha.Month);
                viewModelEvento.Day = evento.Fecha.Day.ToString();
                viewModelEvento.Year = evento.Fecha.Year.ToString();

                List<Zona> listaZonas = serviceZona.GetZonasByIDLugarEvento(evento.LugarEvento).ToList();

                int seatsCount = 0;

                foreach (var item in listaZonas)
                {
                    seatsCount += serviceLugar.GetLugarByEventZone(evento.ID, item.ID)
                        .Where(x => x.Estado.Equals(TypeEstadoAsiento.DISPONIBLE.ToString()))
                        .ToList().Count;
                }

                if (seatsCount == 0)
                    viewModelEvento.isSold = true;


                return View(viewModelEvento);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]
        public ActionResult Mantenimiento()
        {
            Log.Info("Visita Matenimiento de Eventos. " + MethodBase.GetCurrentMethod());
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            Log.Info("Obtiene Eventos: " + MethodBase.GetCurrentMethod());
            List<Evento> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceEvento.GetAllEvents().ToList();
                if (lista != null)
                {
                    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(lista, loadOptions)), "application/json");
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

        [HttpPost]
        public ActionResult Post(string values)
        {
            Log.Info("Crear un evento: " + MethodBase.GetCurrentMethod());
            Evento oEvento = new Evento();
            try
            {
                JsonConvert.PopulateObject(values, oEvento);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceEvento.Save(oEvento) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información.");
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Put(String key, String values)
        {
            Log.Info("Actualiza evento"+ key + MethodBase.GetCurrentMethod());
            Evento oEvento = new Evento();
            try
            {
                // Buscar por Id
                oEvento = serviceEvento.GetEventByID(key);
                // Si no existe
                if (oEvento == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la Evento No. {key}");
                }
                else
                {
                    // Si existe poblar oEvento con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oEvento);
                }


                // Validar el model
                if (!TryValidateModel(oEvento))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceEvento.Save(oEvento) == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. ");
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete(String key)
        {
            Log.Info("Elimina evento"+ key + MethodBase.GetCurrentMethod());
            try
            {
                serviceEvento.DeleteEvento(key);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
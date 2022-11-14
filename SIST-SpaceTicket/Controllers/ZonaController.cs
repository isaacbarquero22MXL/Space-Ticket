using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Catalogo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class ZonaController : Controller
    {
        private IServiceZona serviceZona = new ServiceZona();
        private IServiceLugarEvento serviceLugarEvento = new ServiceLugarEvento();
        
        // GET: Zona
        public ActionResult Index()
        {
            Log.Info("Ejecuta controlador Zona: " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult FindZones(int LugarEvento, String IDEvento)
        {
            Log.Info("Se buscó la zona del evento con ID: " + LugarEvento);
            IEnumerable<Zona> lista = null;
            if (Session["Usuario"] != null && Session["Role"] != null)
            {
                if (Session["Role"].ToString().Equals("Cliente"))
                {
                    try
                    {
                        lista = serviceZona.GetZonasByIDLugarEvento(LugarEvento).OrderByDescending(x => x.Precio);
                        TempData["ZonasEvento"] = lista;
                        TempData["IDEventoTemp"] = IDEvento; // guardamos el evento para buscar los asientos relacionados a ese evento
                        Session["DetalleFactura"] = null; // reseteamos el detalle factura por si se desea realizar la compra nuevamente
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, MethodBase.GetCurrentMethod());
                        TempData["Message"] = ex.Message;
                        TempData["Type"] = "Fail";
                    }
                    return RedirectToAction("Index", "Event", new { IDEvento = IDEvento });
                }
                else
                {
                    return RedirectToAction("NotFound", "Error");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]
        public ActionResult Mantenimiento()
        {
            Log.Info("Ejecuta controlador Zona: " + MethodBase.GetCurrentMethod());
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador Zona: " + MethodBase.GetCurrentMethod());
            List<Zona> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceZona.GetZona().ToList();
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
            Log.Info("Ejecuta controlador Zona: " + MethodBase.GetCurrentMethod());
            Zona oZona = new Zona();
            try
            {
                JsonConvert.PopulateObject(values, oZona);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceZona.Save(oZona) == null)
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
        public ActionResult Put(Int32 key, String values)
        {
            Log.Info("Ejecuta controlador Zona: " + MethodBase.GetCurrentMethod());
            Zona oZona = new Zona();
            try
            {
                // Buscar por Id
                oZona = serviceZona.GetZonaByID(Convert.ToInt32(key));
                // Si no existe
                if (oZona == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la Zona No. {key}");
                }
                else
                {
                    // Si existe poblar oZona con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oZona);
                }


                // Validar el model
                if (!TryValidateModel(oZona))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceZona.Save(oZona) == null)
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
        public ActionResult Delete(Int32 key)
        {
            Log.Info("Ejecuta controlador Zona: " + MethodBase.GetCurrentMethod());
            try
            {
                serviceZona.DeleteZona(Convert.ToInt32(key));

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
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
    public class LugarEventoController : Controller
    {
        private IServiceLugarEvento serviceLugarEvento = new ServiceLugarEvento();
        // GET: LugarEvento
        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]
        public ActionResult Index()
        {
            Log.Info("Visita controlador LugarEvento: " + MethodBase.GetCurrentMethod());
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador LugarEvento: " + MethodBase.GetCurrentMethod());
            List<LugarEvento> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceLugarEvento.GetLugarEvento().ToList();
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
            Log.Info("Ejecuta controlador LugarEvento: " + MethodBase.GetCurrentMethod());
            IServiceLugarEvento serviceLugarEvento = new ServiceLugarEvento();
            LugarEvento oLugarEvento = new LugarEvento();
            try
            {
                JsonConvert.PopulateObject(values, oLugarEvento);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceLugarEvento.Save(oLugarEvento) == null)
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
            Log.Info("Ejecuta controlador LugarEvento: " + MethodBase.GetCurrentMethod());
            LugarEvento oLugarEvento = new LugarEvento();
            try
            {
                // Buscar por Id
                oLugarEvento = serviceLugarEvento.GetLugarEventoByID(Convert.ToInt32(key));
                // Si no existe
                if (oLugarEvento == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la LugarEvento No. {key}");
                }
                else
                {
                    // Si existe poblar oLugarEvento con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oLugarEvento);
                }


                // Validar el model
                if (!TryValidateModel(oLugarEvento))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceLugarEvento.Save(oLugarEvento) == null)
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
            Log.Info("Ejecuta controlador LugarEvento: " + MethodBase.GetCurrentMethod());
            try
            {
                serviceLugarEvento.DeleteLugarEvento(Convert.ToInt32(key));

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
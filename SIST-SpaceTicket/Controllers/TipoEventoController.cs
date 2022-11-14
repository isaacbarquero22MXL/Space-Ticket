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
    public class TipoEventoController : Controller
    {
        private IServiceTipoEvento serviceTipoEvento = new ServiceTipoEvento();
        // GET: TipoEvento
        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]
        public ActionResult Index()
        {
            Log.Info("Ejecuta controlador TipoEvento: " + MethodBase.GetCurrentMethod());
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador TipoEvento: " + MethodBase.GetCurrentMethod());
            List<TipoEvento> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceTipoEvento.GetTipoEvento().ToList();
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
            Log.Info("Ejecuta controlador TipoEvento: " + MethodBase.GetCurrentMethod());
            IServiceTipoEvento serviceTipoEvento = new ServiceTipoEvento();
            TipoEvento oTipoEvento = new TipoEvento();
            try
            {
                JsonConvert.PopulateObject(values, oTipoEvento);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceTipoEvento.Save(oTipoEvento) == null)
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
            Log.Info("Ejecuta controlador TipoEvento: " + MethodBase.GetCurrentMethod());
            TipoEvento oTipoEvento = new TipoEvento();
            try
            {
                // Buscar por Id
                oTipoEvento = serviceTipoEvento.GetTipoEventoByID(Convert.ToInt32(key));
                // Si no existe
                if (oTipoEvento == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la TipoEvento No. {key}");
                }
                else
                {
                    // Si existe poblar oTipoEvento con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oTipoEvento);
                }


                // Validar el model
                if (!TryValidateModel(oTipoEvento))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceTipoEvento.Save(oTipoEvento) == null)
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
            Log.Info("Ejecuta controlador TipoEvento: " + MethodBase.GetCurrentMethod());
            try
            {
                serviceTipoEvento.DeleteTipoEvento(Convert.ToInt32(key));

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
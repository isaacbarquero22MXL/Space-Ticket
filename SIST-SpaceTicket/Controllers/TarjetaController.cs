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
    public class TarjetaController : Controller
    {
        // GET: TipoTarjeta
        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]
        public ActionResult Index()
        {
            Log.Info($"Visita {this.GetType().Name} - {MethodBase.GetCurrentMethod()}");
            return View();
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            IServiceTarjeta serviceTipoTarjeta = new ServiceTarjeta();
            List<TipoTarjeta> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceTipoTarjeta.GetTipoTarjeta().ToList();
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
            IServiceTarjeta serviceTipoTarjeta = new ServiceTarjeta();
            TipoTarjeta oTipoTarjeta = new TipoTarjeta();
            try
            {
                JsonConvert.PopulateObject(values, oTipoTarjeta);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceTipoTarjeta.Save(oTipoTarjeta) == null)
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
            IServiceTarjeta serviceTipoTarjeta = new ServiceTarjeta();
            TipoTarjeta oTipoTarjeta = new TipoTarjeta();
            try
            {
                // Buscar por Id
                oTipoTarjeta = serviceTipoTarjeta.GetTipoTarjetaByID(Convert.ToInt32(key));
                // Si no existe
                if (oTipoTarjeta == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la TipoTarjeta No. {key}");
                }
                else
                {
                    // Si existe poblar oTipoTarjeta con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oTipoTarjeta);
                }


                // Validar el model
                if (!TryValidateModel(oTipoTarjeta))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceTipoTarjeta.Save(oTipoTarjeta) == null)
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
            IServiceTarjeta serviceTipoTarjeta = new ServiceTarjeta();
            try
            {
                serviceTipoTarjeta.DeleteTipoTarjeta(Convert.ToInt32(key));

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
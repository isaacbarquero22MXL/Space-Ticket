using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Catalogo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class TelefonoController : Controller
    {

        private IServiceEmpleadoTelefono serviceEmpleadoTelefono = new ServiceEmpleadoTelefono();

        // GET: Telefono
        public ActionResult Index()
        {
            Log.Info("Ejecuta controlador telefono: " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult Get(string IDEmpleado, DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador telefono: " + MethodBase.GetCurrentMethod());
            List<EmpleadoTelefono> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceEmpleadoTelefono.GetEmpleadoTelefonoByIDEmpleado(IDEmpleado).ToList();
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

        public object GetEmpleadoTelefonoByIDEmpleado(string IDEmpleado, DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador telefono: " + MethodBase.GetCurrentMethod());
            List<EmpleadoTelefono> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceEmpleadoTelefono.GetEmpleadoTelefonoByIDEmpleado(IDEmpleado).ToList();
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
            Log.Info("Ejecuta controlador telefono: " + MethodBase.GetCurrentMethod());
            EmpleadoTelefono oEmpleadoTelefono = new EmpleadoTelefono();

            try
            {
                JsonConvert.PopulateObject(values, oEmpleadoTelefono);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceEmpleadoTelefono.Save(oEmpleadoTelefono) == null)
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
        public ActionResult Put(string key, String values)
        {
            Log.Info("Ejecuta controlador telefono: " + MethodBase.GetCurrentMethod());
            EmpleadoTelefono oEmpleadoTelefono = new EmpleadoTelefono();

            try
            {
                // Crear Objeto dinámico para extraer los campos del JSON
                // Observe que ambos son los campos del Grid que son llave
                // primaria del Detalle 
                JObject parameters = JObject.Parse(key);
                int secuencia = int.Parse(parameters["ID"].ToString());
                int IDUsuario = int.Parse(parameters["IDEmpleado"].ToString());

                // Buscar por Id
                oEmpleadoTelefono = serviceEmpleadoTelefono.GetEmpleadoTelefonoByID(secuencia);
                // Si no existe
                if (oEmpleadoTelefono == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la EmpleadoTelefono No. {key}");
                }
                else
                {
                    // Si existe poblar oEmpleadoTelefono con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oEmpleadoTelefono);
                }


                // Validar el model
                if (!TryValidateModel(oEmpleadoTelefono))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceEmpleadoTelefono.Save(oEmpleadoTelefono) == null)
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
        public ActionResult Delete(string key)
        {
            Log.Info("Ejecuta controlador telefono: " + MethodBase.GetCurrentMethod());
            try
            {
                // Crear Objeto dinámico para extraer los campos del JSON
                // Observe que ambos son los campos del Grid que son llave
                // primaria del Detalle 
                JObject parameters = JObject.Parse(key);
                int secuencia = int.Parse(parameters["ID"].ToString());
                int idFactura = int.Parse(parameters["IDEmpleado"].ToString());

                serviceEmpleadoTelefono.DeleteEmpleadoTelefono(secuencia);

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
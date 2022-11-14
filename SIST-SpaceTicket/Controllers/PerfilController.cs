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
    public class PerfilController : Controller
    {
        private IServiceEmpleadoPerfil serviceEmpleadoPerfil = new ServiceEmpleadoPerfil();
        // GET: Perfil
        public ActionResult Index()
        {
            Log.Info("Ejecuta controlador LugarEvento: " + MethodBase.GetCurrentMethod());
            return View();
        }

        // ************************************************
        // *******        EMPLEADO PERFIL           *******
        // ************************************************

        public ActionResult Get(string IDEmpleado, DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador Get Perfil por empleado: " + IDEmpleado + MethodBase.GetCurrentMethod());
            List<EmpleadoPerfil> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceEmpleadoPerfil.GetEmpleadoPerfilByIDEmpleado(IDEmpleado).ToList();
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

        public object GetEmpleadoPerfilByIDEmpleado(string IDEmpleado, DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador get Perfil para empleado: " + IDEmpleado + MethodBase.GetCurrentMethod());
            List<EmpleadoPerfil> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceEmpleadoPerfil.GetEmpleadoPerfilByIDEmpleado(IDEmpleado).ToList();
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
            Log.Info("Ejecuta controlador Perfil: " + MethodBase.GetCurrentMethod());
            EmpleadoPerfil oEmpleadoPerfil = new EmpleadoPerfil();

            try
            {
                JsonConvert.PopulateObject(values, oEmpleadoPerfil);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceEmpleadoPerfil.Save(oEmpleadoPerfil) == null)
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
            Log.Info("Ejecuta controlador Perfil: " + MethodBase.GetCurrentMethod());
            EmpleadoPerfil oEmpleadoPerfil = new EmpleadoPerfil();

            try
            {
                // Crear Objeto dinámico para extraer los campos del JSON
                // Observe que ambos son los campos del Grid que son llave
                // primaria del Detalle 
                JObject parameters = JObject.Parse(key);
                int secuencia = int.Parse(parameters["ID"].ToString());
                int IDUsuario = int.Parse(parameters["IDEmpleado"].ToString());

                // Buscar por Id
                oEmpleadoPerfil = serviceEmpleadoPerfil.GetEmpleadoPerfilByID(secuencia);
                // Si no existe
                if (oEmpleadoPerfil == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la EmpleadoPerfil No. {key}");
                }
                else
                {
                    // Si existe poblar oEmpleadoPerfil con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oEmpleadoPerfil);
                }


                // Validar el model
                if (!TryValidateModel(oEmpleadoPerfil))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceEmpleadoPerfil.Save(oEmpleadoPerfil) == null)
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
            Log.Info("Ejecuta controlador Perfil: " + MethodBase.GetCurrentMethod());
            try
            {
                // Crear Objeto dinámico para extraer los campos del JSON
                // Observe que ambos son los campos del Grid que son llave
                // primaria del Detalle 
                JObject parameters = JObject.Parse(key);
                int secuencia = int.Parse(parameters["ID"].ToString());
                int idFactura = int.Parse(parameters["IDEmpleado"].ToString());

                serviceEmpleadoPerfil.DeleteEmpleadoPerfil(secuencia);

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
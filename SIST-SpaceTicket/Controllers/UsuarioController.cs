using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Catalogo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIST_SpaceTicket.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class UsuarioController : Controller
    {

        private IServiceCliente serviceCliente = new ServiceCliente();
        private IServiceEmpleado serviceEmpleado = new ServiceEmpleado();

        // ************************************************
        // *******             CLIENTE              *******
        // ************************************************
        // GET: Usuario
        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]
        public ActionResult IndexCliente()
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            return View();
        }

        [HttpGet]
        public object GetCliente(DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            List<Cliente> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceCliente.GetCliente().ToList();
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
        public ActionResult PostCliente(string values)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            IServiceCliente serviceCliente = new ServiceCliente();
            Cliente oCliente = new Cliente();
            try
            {
                JsonConvert.PopulateObject(values, oCliente);

                if (!ModelState.IsValid)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo salvar la información. [ModelState]");
                }

                if (serviceCliente.Save(oCliente) == null)
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
        public ActionResult PutCliente(String key, String values)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            Cliente oCliente = new Cliente();
            try
            {
                JObject parameters = JObject.Parse(key);
                string codigoCliente = parameters["CodigoCliente"].ToString();
                string cedula = parameters["Cedula"].ToString();
                // Buscar por Id
                oCliente = serviceCliente.GetClienteByID(cedula);
                // Si no existe
                if (oCliente == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la Cliente No. {key}");
                }
                else
                {
                    // Si existe poblar oCliente con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oCliente);
                }


                // Validar el model
                if (!TryValidateModel(oCliente))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceCliente.Save(oCliente) == null)
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
        public ActionResult DeleteCliente(String key)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            try
            {
                throw new Exception("No se puede ejecutar este proceso.");
                //JObject parameters = JObject.Parse(key);
                //string codigoCliente = parameters["CodigoCliente"].ToString();
                //string cedula = parameters["Cedula"].ToString();
                //serviceCliente.DeleteCliente(cedula);

                //return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // ************************************************
        // *******             EMPLEADO              *******
        // ************************************************

        [Authorize(Roles = "ADMINISTRADOR, OPERADOR")]
        public ActionResult IndexEmpleado()
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            return View();
        }

        [HttpGet]
        public object GetEmpleado(DataSourceLoadOptions loadOptions)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            List<Empleado> lista = null;

            try
            {
                loadOptions.SortByPrimaryKey = false;
                lista = serviceEmpleado.GetEmpleado().ToList();
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
        public ActionResult PostEmpleado(string values)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            Empleado oEmpleado = new Empleado();
            try
            {
                JsonConvert.PopulateObject(values, oEmpleado);

                if (!TryValidateModel(oEmpleado))
                {
                    String errors = ModelHelper.GetModelError(ModelState);

                    throw new Exception(errors);
                }

                if (serviceEmpleado.Save(oEmpleado) == null)
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
        public ActionResult PutEmpleado(String key, String values)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            Empleado oEmpleado = new Empleado();
            try
            {
                // Buscar por Id
                oEmpleado = serviceEmpleado.GetEmpleadoByID(key);
                // Si no existe
                if (oEmpleado == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"No existe la Cliente No. {key}");
                }
                else
                {
                    // Si existe poblar oCliente con los values osea los properties que se actualizaron.
                    JsonConvert.PopulateObject(values, oEmpleado);
                }


                // Validar el model
                if (!TryValidateModel(oEmpleado))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se pudo actualizar la información. [ModelState]");

                // Salvar / Actualizar
                if (serviceEmpleado.Save(oEmpleado) == null)
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
        public ActionResult DeleteEmpleado(String key)
        {
            Log.Info("Ejecuta controlador Usuario: " + MethodBase.GetCurrentMethod());
            try
            {
                throw new Exception("No se puede ejecutar este proceso.");
                //JObject parameters = JObject.Parse(key);
                //string codigoCliente = parameters["CodigoCliente"].ToString();
                //string cedula = parameters["Cedula"].ToString();
                //serviceCliente.DeleteCliente(cedula);

                //return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
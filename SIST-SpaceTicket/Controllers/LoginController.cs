using ApplicationCore.Services;
using Infraestructure.Models.Catalogo;
using SIST_SpaceTicket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SIST_SpaceTicket.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Log.Info("Visita Login: " + MethodBase.GetCurrentMethod());
            return View();
        }

        public ActionResult Login(ViewModelLogin login)
        {
            IServiceCliente serviceCliente = new ServiceCliente();
            IServiceEmpleado serviceEmpleado = new ServiceEmpleado();

            try
            {
                Log.Info($"Login {login.Correo}");

                //DataModel db = new DataModel();
                if (!string.IsNullOrEmpty(login.Correo) && !string.IsNullOrEmpty(login.Contrasenna))
                {
                    // var user = db.Usuario.FirstOrDefault(e => e.Login == codigo.Trim().ToString() && e.Password == contrasena.Trim().ToString());
                    Empleado empleado = serviceEmpleado.Login(login.Correo, login.Contrasenna);
                    if (empleado != null)
                    {
                        List<String> roles = new Infraestructure.Models.Seguridad.UserRoleProvider()
                                           .GetRolesForUser(empleado.Cedula).Where(x => !x.Equals("Ninguno")).ToList();
                        FormsAuthentication.SetAuthCookie(empleado.Cedula, true);
                        Session["Usuario"] = empleado;
                        if (roles.Count > 1)
                        {
                            Session["ViewRoles"] = roles;
                            Session["isSwitching"] = false; // falso por que se esta logueando por primera vez
                            return RedirectToAction("SelectRole");
                        }
                        else
                        {
                            return RedirectToAction("LogWithRole", "Login", new { role = roles[0], isSwitching = false });
                        }
                    }
                    else
                    {
                        // si no existe el Empleado se comprueba si un empleado trata de ingresar
                        Cliente cliente = serviceCliente.Login(login.Correo, login.Contrasenna);
                        if (cliente != null)
                        {
                            Session["Usuario"] = cliente;
                            Session["Role"] = serviceCliente.GetRolesForUser(cliente.Cedula);
                            FormsAuthentication.SetAuthCookie(cliente.CodigoCliente, true);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            string mensaje = "Los datos no corresponden con un usuario registrado";
                            Log.Error($"Login {login.Correo} : No encontramos tus datos");
                            TempData["Message"] = mensaje;
                            TempData["Type"] = "Fail";
                            return RedirectToAction("Index", "Login", new { message = mensaje });
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(login.Correo))
                    {
                        return RedirectToAction("Index", "Login", new { message = "El usuario es requerido" });
                    }
                    else if (string.IsNullOrEmpty(login.Contrasenna))
                    {
                        return RedirectToAction("Index", "Login", new { message = "La contraseña es requerida" });
                    }
                    else
                    {
                        Log.Error($"Login {login.Correo} :Los valores de usuario y contraseña deben estar llenos");
                        return RedirectToAction("Index", "Login", new { message = "Los valores de usuario y contraseña deben estar llenos" });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Type"] = "Fail";
                return View("Index");
            }
        }

        public ActionResult SelectRole()
        {
            Log.Info("Selecciona perfil: " + MethodBase.GetCurrentMethod());
            if (Session["ViewRoles"] != null)
            {
                ViewModelRoles viewModelRoles = new ViewModelRoles();
                viewModelRoles.Roles = (List<String>)Session["ViewRoles"];
                return View(viewModelRoles);
            }
            else
            {
                String mensaje = "Se ha entrado a seleccionar perfil sin iniciar sesión.";
                TempData["Message"] = mensaje;
                TempData["Type"] = "Fail";
                Log.Error(new Exception(mensaje), MethodBase.GetCurrentMethod());
                return RedirectToAction("NotFound", "Error");
            }
        }

        public ActionResult LogWithRole(String role, bool isSwitching)
        {
            Log.Info("Se intenta loguear con el rol: "+ role + MethodBase.GetCurrentMethod());
            if (role.Equals("Cliente")) // si es cliente se debe cambiar la sesión de empleado a cliente
            {
                Empleado empleado = (Empleado)Session["Usuario"]; // se obtiene la sesion del empleado
                IServiceCliente serviceCliente = new ServiceCliente(); // se crea una instancia del servicio cliente
                Session["Usuario"] = serviceCliente.GetClienteByID(empleado.Cedula); // se cambia el usuario por cliente
                Session["Role"] = serviceCliente.GetRolesForUser(empleado.Cedula); // se cambia el rol actual a Cliente
                FormsAuthentication.SetAuthCookie(((Cliente)Session["Usuario"]).CodigoCliente, true);
            }
            else
            {
                // si se esta cambiando el rol desde el inicio se valida si es verdad
                if (isSwitching)
                {
                    String oldRole = Session["Role"].ToString();
                    Session["Role"] = role; // se cambia el rol actual al elegido
                    if (!oldRole.Equals(role))
                    {
                        if (oldRole.Equals("Cliente"))
                        {
                            Cliente cliente = (Cliente)Session["Usuario"]; // se obtiene la sesion del cliente
                            IServiceEmpleado serviceEmpleado = new ServiceEmpleado(); // se crea una instancia del servicio empleado
                            Session["Usuario"] = serviceEmpleado.GetEmpleadoByID(cliente.Cedula); // se cambia el usuario por empleado
                            FormsAuthentication.SetAuthCookie(((Empleado)Session["Usuario"]).Cedula, true);
                        }
                    }
                }
                else
                {
                    Session["Role"] = role; // se cambia el rol actual al elegido
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SwtichRole()
        {
            Log.Info("Cambia de rol: " + MethodBase.GetCurrentMethod());
            Session["isSwitching"] = true;
            return RedirectToAction("SelectRole");
        }

        public ActionResult Logout()
        {
            Log.Info("Se desloguea: " + MethodBase.GetCurrentMethod());
            Session["Usuario"] = null;
            return View("Index");
        }
    }
}
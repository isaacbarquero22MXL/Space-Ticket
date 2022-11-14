using ApplicationCore.Services;
using Infraestructure.Models.Catalogo;
using SIST_SpaceTicket.Validation;
using SIST_SpaceTicket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {

            if(TempData["ViewModelTemporal"] != null)
            {
                ViewModelCliente cliente = (ViewModelCliente)TempData["ViewModelTemporal"];
                cliente.Contrasenna = "";
                cliente.ContrasennaConfirm = "";
                return View(cliente);
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(ViewModelCliente cliente) {
            //declara e instancia la clase interes
            IServiceCliente service = new ServiceCliente();
            Cliente result;
            try
            {
                if (TryValidateModel(cliente))
                {
                    if (!cliente.Contrasenna.Equals(cliente.ContrasennaConfirm))
                    {
                        throw new Exception("Las claves no coinciden.");
                    }

                    result = service.Save(CreateInstance(cliente));
                    if (result != null)
                    {
                        TempData["Message"] = "Cliente " + result.Nombre + " " + result.Apellido1 + " registrado correctamente.";
                        TempData["Type"] = "Success";
                    }
                }
                else
                {
                    string errors = ModelHelper.GetModelError(ModelState);
                    throw new Exception(errors);
                }
                TempData["ViewModelTemporal"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                TempData["ViewModelTemporal"] = cliente;
                TempData["Message"] = ex.Message;
                TempData["Type"] = "Fail";
                return RedirectToAction("Index");
            }
        }

        public Cliente CreateInstance(ViewModelCliente cliente)
        {
            return new Cliente() {
                // se eliminan espacios
                Cedula = cliente.Cedula.Trim(),
                Nombre = cliente.Nombre.Trim(),
                Apellido1 = cliente.Apellido1.Trim(),
                Apellido2 = cliente.Apellido2.Trim(),
                Correo = cliente.Correo.Trim(),
                Contrasenna = cliente.Contrasenna.Trim(),
                FechaNac = cliente.FechaNac,
                Nacionalidad = cliente.Nacionalidad.Trim(),
                Estado = TypeEstado.ACTIVO.ToString(),
                Sexo = cliente.Sexo,
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SIST_SpaceTicket.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            Log.Info("Error de autorización, cayó en el método: " + MethodBase.GetCurrentMethod());
            return View();
        }
    }
}
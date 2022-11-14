using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Catalogo;
using Infraestructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIST_SpaceTicket.ApiControllers
{
    public class ApiHoraController : ApiController
    {
        [HttpGet]
        [Route("Horas/horas", Name = "horas")]
        public HttpResponseMessage GetPaises(DataSourceLoadOptions loadOptions)
        {
            try
            {
                IEnumerable<Hora> lista;
                IServiceHora _ServiceHora = new ServiceHora();
                lista = _ServiceHora.GetAllHoras();
                return Request.CreateResponse(DataSourceLoader.Load(lista, loadOptions));
                //return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Redireccion a la captura del Error
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}

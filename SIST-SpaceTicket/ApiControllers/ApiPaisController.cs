using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIST_SpaceTicket.ApiControllers
{
    public class ApiPaisController : ApiController
    {
        [HttpGet]
        [Route("pais/paises", Name = "paises")]
        public HttpResponseMessage GetPaises(DataSourceLoadOptions loadOptions)
        {
            try
            {
                IEnumerable<Pais> lista;
                IServicePais _ServicePais = new ServicePais();
                lista = _ServicePais.GetAllPaises();
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

using ApplicationCore.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Infraestructure.Models.Direccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SIST_SpaceTicket.ApiControllers
{
    public class ApiDireccionController : ApiController
    {
        [HttpGet]
        [Route("direccion/pronvicias", Name = "provincias")]
        public HttpResponseMessage GetProvincias(DataSourceLoadOptions loadOptions)
        {
            try
            {
                IEnumerable<Provincia> lista;
                IServiceDireccion _ServiceDireccion = new ServiceDireccion();
                lista = _ServiceDireccion.GetAllProvicnias();
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

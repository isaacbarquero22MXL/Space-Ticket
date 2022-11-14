using Infraestructure.Models.Direccion;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDireccion : IServiceDireccion
    {
        IRepositoryDireccion _RepositoryDireccion = new RepositoryDireccion();
        public IEnumerable<Canton> GetAllCantones()
        {
            return _RepositoryDireccion.GetAllCantones();
        }

        public IEnumerable<Distrito> GetAllDistritos()
        {
            return _RepositoryDireccion.GetAllDistritos();
        }

        public IEnumerable<Provincia> GetAllProvicnias()
        {
            return _RepositoryDireccion.GetAllProvicnias();
        }
    }
}

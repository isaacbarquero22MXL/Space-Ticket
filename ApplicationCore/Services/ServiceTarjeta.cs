using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTarjeta : IServiceTarjeta
    {
        private IRepositoryTarjeta _RepositoryTarjeta = new RepositoryTarjeta();
        public void DeleteTipoTarjeta(int id)
        {
           _RepositoryTarjeta.DeleteTipoTarjeta(id);
        }

        public IEnumerable<TipoTarjeta> GetTipoTarjeta()
        {
            return _RepositoryTarjeta.GetTipoTarjeta();
        }

        public TipoTarjeta GetTipoTarjetaByID(int id)
        {
            return _RepositoryTarjeta.GetTipoTarjetaByID(id);  
        }

        public TipoTarjeta Save(TipoTarjeta TipoTarjeta)
        {
            return _RepositoryTarjeta.Save(TipoTarjeta);
        }
    }
}

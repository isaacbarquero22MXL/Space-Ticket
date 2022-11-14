using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoEvento : IServiceTipoEvento
    {
        private IRepositoryTipoEvento _RepositoryTipoEvento = new RepositoryTipoEvento();

        public void DeleteTipoEvento(int id)
        {
            _RepositoryTipoEvento.DeleteTipoEvento(id);
        }

        public IEnumerable<TipoEvento> GetTipoEvento()
        {
            return _RepositoryTipoEvento.GetTipoEvento();
        }

        public TipoEvento GetTipoEventoByID(int id)
        {
            return _RepositoryTipoEvento.GetTipoEventoByID(id);
        }

        public TipoEvento Save(TipoEvento TipoEvento)
        {
            return _RepositoryTipoEvento.Save(TipoEvento);
        }
    }
}

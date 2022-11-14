using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceLugarEvento: IServiceLugarEvento
    {
        private IRepositoryLugarEvento _RepositoryLugarEvento = new RepositoryLugarEvento();

        public void DeleteLugarEvento(int id)
        {
             _RepositoryLugarEvento.DeleteLugarEvento(id);
        }

        public IEnumerable<LugarEvento> GetLugarEvento()
        {
            return _RepositoryLugarEvento.GetLugarEvento();
        }

        public LugarEvento GetLugarEventoByID(int id)
        {
            return _RepositoryLugarEvento.GetLugarEventoByID(id);
        }

        public LugarEvento Save(LugarEvento LugarEvento)
        {
            return _RepositoryLugarEvento.Save(LugarEvento);
        }
    }
}

using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryLugarEvento
    {
        IEnumerable<LugarEvento> GetLugarEvento();
        LugarEvento GetLugarEventoByID(int id);
        void DeleteLugarEvento(int id);
        LugarEvento Save(LugarEvento LugarEvento);
    }
}

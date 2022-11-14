using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceLugarEvento
    {
        IEnumerable<LugarEvento> GetLugarEvento();
        LugarEvento GetLugarEventoByID(int id);
        void DeleteLugarEvento(int id);
        LugarEvento Save(LugarEvento LugarEvento);
    }
}

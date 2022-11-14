using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEvento
    {
        IEnumerable<Evento> GetAllEvents();

        Evento GetEventByID(string ID);

        void DeleteEvento(string id);
        Evento Save(Evento evento);
        bool Existe(string codigo);
    }
}

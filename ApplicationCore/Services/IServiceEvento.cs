using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEvento
    {
        IEnumerable<Evento> GetAllEvents();

        Evento GetEventByID(string ID);

        void DeleteEvento(string id);
        Evento Save(Evento evento);
        bool Existe(string codigo);
    }
}

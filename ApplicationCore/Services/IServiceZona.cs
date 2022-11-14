using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceZona
    {
        IEnumerable<Zona> GetZona();
        Zona GetZonaByID(int id);

        IEnumerable<Zona> GetZonasByIDLugarEvento(int id);
        void DeleteZona(int id);
        Zona Save(Zona Zona);
        bool Existe(int codigo);
    }
}

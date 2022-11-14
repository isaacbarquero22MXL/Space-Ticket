using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceLugar
    {
        IEnumerable<Lugar> GetLugarByEventZone(string IDEvento, int IDZona);

        IEnumerable<Lugar> GetLugar();
        Lugar GetLugarByID(int id);
        void DeleteLugar(int id);
        Lugar Save(Lugar Lugar);
        bool Existe(String codigo);
    }
}

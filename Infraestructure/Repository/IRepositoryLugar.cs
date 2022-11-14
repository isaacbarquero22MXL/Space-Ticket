using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryLugar
    {
        IEnumerable<Lugar> GetLugarByEventZone(string IDEvento, int IDZona);

        IEnumerable<Lugar> GetLugar();
        Lugar GetLugarByID(int id);

        Lugar GetLugarByZoneRowAndSeat(string IDZona, string Row, string seat);
        void DeleteLugar(int id);
        Lugar Save(Lugar Lugar);
        bool Existe(int codigo);
    }
}

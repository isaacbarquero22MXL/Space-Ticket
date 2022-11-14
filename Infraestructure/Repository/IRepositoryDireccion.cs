using Infraestructure.Models.Direccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryDireccion
    {
        IEnumerable<Provincia> GetAllProvicnias();

        IEnumerable<Canton> GetAllCantones();

        IEnumerable<Distrito> GetAllDistritos();
    }
}

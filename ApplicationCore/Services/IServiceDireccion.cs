using Infraestructure.Models.Direccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceDireccion
    {
        IEnumerable<Provincia> GetAllProvicnias();

        IEnumerable<Canton> GetAllCantones();

        IEnumerable<Distrito> GetAllDistritos();
    }
}

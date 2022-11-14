using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEmpleadoTelefono
    {
        IEnumerable<EmpleadoTelefono> GetEmpleadoTelefono();

        EmpleadoTelefono GetEmpleadoTelefonoByID(int ID);
        IEnumerable<EmpleadoTelefono> GetEmpleadoTelefonoByIDEmpleado(string IDEmpleado);
        void DeleteEmpleadoTelefono(int id);
        EmpleadoTelefono Save(EmpleadoTelefono EmpleadoTelefono);
    }
}

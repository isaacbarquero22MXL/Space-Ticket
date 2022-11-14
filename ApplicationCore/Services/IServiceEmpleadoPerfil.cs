using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEmpleadoPerfil
    {
        IEnumerable<EmpleadoPerfil> GetEmpleadoPerfil();

        EmpleadoPerfil GetEmpleadoPerfilByID(int ID);
        IEnumerable<EmpleadoPerfil> GetEmpleadoPerfilByIDEmpleado(string IDEmpleado);
        void DeleteEmpleadoPerfil(int id);
        EmpleadoPerfil Save(EmpleadoPerfil EmpleadoPerfil);
    }
}

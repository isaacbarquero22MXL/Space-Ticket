using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEmpleadoPerfil
    {
        IEnumerable<EmpleadoPerfil> GetEmpleadoPerfil();

        EmpleadoPerfil GetEmpleadoPerfilByID(int ID);

        EmpleadoPerfil GetEmpleadoPerfilByIDEmpleado(int IDPerfil, string IDEmpleado);
        IEnumerable<EmpleadoPerfil> GetEmpleadoPerfilByIDEmpleado(string IDEmpleado);

        void DeleteEmpleadoPerfil(int id);
        EmpleadoPerfil Save(EmpleadoPerfil EmpleadoPerfil);
    }
}

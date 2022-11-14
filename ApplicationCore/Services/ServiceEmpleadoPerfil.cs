using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEmpleadoPerfil : IServiceEmpleadoPerfil
    {
        private IRepositoryEmpleadoPerfil _RepositoryEmpleadoPerfil = new RepositoryEmpleadoPerfil();

        public void DeleteEmpleadoPerfil(int id)
        {
            _RepositoryEmpleadoPerfil.DeleteEmpleadoPerfil(id);
        }

        public IEnumerable<EmpleadoPerfil> GetEmpleadoPerfil()
        {
            return _RepositoryEmpleadoPerfil.GetEmpleadoPerfil();
        }

        public EmpleadoPerfil GetEmpleadoPerfilByID(int ID)
        {
            return _RepositoryEmpleadoPerfil.GetEmpleadoPerfilByID(ID);
        }

        public IEnumerable<EmpleadoPerfil> GetEmpleadoPerfilByIDEmpleado(string IDEmpleado)
        {
            return _RepositoryEmpleadoPerfil.GetEmpleadoPerfilByIDEmpleado(IDEmpleado);
        }

        public EmpleadoPerfil Save(EmpleadoPerfil EmpleadoPerfil)
        {
            return _RepositoryEmpleadoPerfil.Save(EmpleadoPerfil);
        }
    }
}

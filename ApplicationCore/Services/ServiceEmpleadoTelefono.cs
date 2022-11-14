using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEmpleadoTelefono : IServiceEmpleadoTelefono
    {
        private IRepositoryEmpleadoTelefono _RepositoryEmpleadoTelefono = new RepositoryEmpleadoTelefono();
        public void DeleteEmpleadoTelefono(int id)
        {
            _RepositoryEmpleadoTelefono.DeleteEmpleadoTelefono(id);
        }

        public IEnumerable<EmpleadoTelefono> GetEmpleadoTelefono()
        {
            return _RepositoryEmpleadoTelefono.GetEmpleadoTelefono();
        }

        public EmpleadoTelefono GetEmpleadoTelefonoByID(int ID)
        {
            return _RepositoryEmpleadoTelefono.GetEmpleadoTelefonoByID(ID);
        }

        public IEnumerable<EmpleadoTelefono> GetEmpleadoTelefonoByIDEmpleado(string IDEmpleado)
        {
            return _RepositoryEmpleadoTelefono.GetEmpleadoTelefonoByIDEmpleado(IDEmpleado);
        }

        public EmpleadoTelefono Save(EmpleadoTelefono EmpleadoTelefono)
        {
            return _RepositoryEmpleadoTelefono.Save(EmpleadoTelefono);
        }
    }
}

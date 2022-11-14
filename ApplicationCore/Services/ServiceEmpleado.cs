using ApplicationCore.Util;
using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEmpleado : IServiceEmpleado
    {
        IRepositoryEmpleado _RepositoryEmpleado = new RepositoryEmpleado();
        public void DeleteEmpleado(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empleado> GetEmpleado()
        {
            List<Empleado> lista = _RepositoryEmpleado.GetEmpleado().ToList();
            foreach (Empleado empleado in lista)
            {
                empleado.Contrasenna = Cryptography.DecrypthAES(empleado.Contrasenna);
            }
            return lista;
        }

        public Empleado GetEmpleadoByID(string id)
        {
            Empleado empleado = _RepositoryEmpleado.GetEmpleadoByID(id);
            empleado.Contrasenna = Cryptography.DecrypthAES(empleado.Contrasenna);
            return empleado;
        }

        public string[] GetRolesForUser(string username)
        {
            return _RepositoryEmpleado.GetRolesForUser(username);   
        }

        public Empleado Login(string email, string password)
        {
            password = Cryptography.EncrypthAES(password); // encripta la contraseña para comparar
            return _RepositoryEmpleado.Login(email, password);
        }

        public Empleado Save(Empleado Empleado)
        {
            // se encripta la contraseña cuando se registra o se actualiza
            Empleado.Contrasenna = Cryptography.EncrypthAES(Empleado.Contrasenna);
            return _RepositoryEmpleado.Save(Empleado);
        }
    }
}

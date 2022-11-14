using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEmpleado
    {
        IEnumerable<Empleado> GetEmpleado();
        Empleado GetEmpleadoByID(string id);
        void DeleteEmpleado(string id);
        Empleado Save(Empleado Empleado);
        string[] GetRolesForUser(string username);

        Empleado Login(string email, string password);
    }
}

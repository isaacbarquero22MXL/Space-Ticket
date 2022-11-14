using Infraestructure.Models.Catalogo;
using Infraestructure.Models.MasterClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEmpleado
    {

        IEnumerable<Empleado> GetEmpleado();
        Empleado GetEmpleadoByID(string id);
        void DeleteEmpleado(string id);
        Empleado Save(Empleado Empleado);
        string[] GetRolesForUser(string username);

        Empleado Login(string email, string password);

        bool Existe(string IDEmpleado);
    }
}

using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryCliente
    {
        IEnumerable<Cliente> GetCliente();
        Cliente GetClienteByID(string id);
        void DeleteCliente(string id);
        Cliente Save(Cliente Cliente);
        string GetRolesForUser(string username);

        Cliente Login(string email, string password);

        bool Existe(String codigo);
    }
}

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
    public class ServiceCliente : IServiceCliente
    {
        private IRepositoryCliente _RepositoryCliente = new RepositoryCliente();
        private static int Codigo = 1;
        private static string CodigoCliente;
        public void DeleteCliente(string id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(string codigo)
        {
            return _RepositoryCliente.Existe(codigo);
        }

        public IEnumerable<Cliente> GetCliente()
        {
            List<Cliente> lista = _RepositoryCliente.GetCliente().ToList();
            foreach (Cliente cliente in lista)
            {
                cliente.Contrasenna = Cryptography.DecrypthAES(cliente.Contrasenna);
            }
            return lista;
        }

        public Cliente GetClienteByID(string id)
        {
            Cliente Cliente = _RepositoryCliente.GetClienteByID(id);
            Cliente.Contrasenna = Cryptography.DecrypthAES(Cliente.Contrasenna);
            return Cliente;
        }

        public string GetRolesForUser(string username)
        {
            return _RepositoryCliente.GetRolesForUser(username);
        }

        public Cliente Login(string email, string password)
        {
            password = Cryptography.EncrypthAES(password); //encripta la contraseña para comparar
            return _RepositoryCliente.Login(email, password);
        }

        public Cliente Save(Cliente Cliente)
        {
            // se encripta cuando se registra o actualiza un cliente
            Cliente.Contrasenna = Cryptography.EncrypthAES(Cliente.Contrasenna);
            if (String.IsNullOrEmpty(Cliente.CodigoCliente))
            {
                GenerateClienteCode(Cliente);
                Cliente.CodigoCliente = CodigoCliente;
            }
            return _RepositoryCliente.Save(Cliente);
        }

        public void GenerateClienteCode(Cliente cliente)
        {
            string code = "C" + Codigo;
            Codigo++;
            if (!Existe(cliente.Cedula))
            {
                // si no existe retorne el codigo de cliente
                if (!Existe(code))
                {
                    CodigoCliente = code;
                }
                else
                {
                    // si el codigo existe recursivamente invoquese hasta que encuentra uno que no exista
                    GenerateClienteCode(cliente);
                }
            }
            else
            {
                throw new Exception("El identificador de este usuario ya se encuentra registrado");
            }
        }
    }
}

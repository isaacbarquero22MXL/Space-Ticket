using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Infraestructure.Models.Seguridad
{
    public class UserRoleProvider : RoleProvider
    {

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            IRepositoryEmpleado repositoryEmpleado = new RepositoryEmpleado(); //instancia repositorio Empleado
            IRepositoryCliente repositoryCliente = new RepositoryCliente(); //instancia repositorio Cliente

            List<string> userRoles = new List<string>(); // Lista de roles

            userRoles = repositoryEmpleado.GetRolesForUser(username).ToList(); // obtiene los roles de Empleado o Ninguno
            if(repositoryCliente.Existe(username))
            {
                userRoles.Add(repositoryCliente.GetRolesForUser(username)); // obtiene el rol Cliente o Ninguno
            }

            return userRoles.ToArray(); // retorna la lista en un arreglo.
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
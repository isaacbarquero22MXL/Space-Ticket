using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePerfil : IServicePerfil
    {
        private IRepositoryPerfil _RepositoryPerfil = new RepositoryPerfil();
        public void DeletePerfil(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Perfil> GetPerfil()
        {
            return _RepositoryPerfil.GetPerfil();
        }

        public Perfil GetPerfilByID(int ID)
        {
            return _RepositoryPerfil.GetPerfilByID(ID);
        }

        public Perfil Save(Perfil Perfil)
        {
            throw new NotImplementedException();
        }
    }
}

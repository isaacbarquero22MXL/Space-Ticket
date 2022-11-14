using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePais : IServicePais
    {
        private IRepositoryPais repositoryPais = new RepositoryPais();
        public IEnumerable<Pais> GetAllPaises()
        {
            return repositoryPais.GetAllPaises();
        }
    }
}

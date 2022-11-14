using Infraestructure.Models.Data;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceHora : IServiceHora
    {
        private IRepositoryHora _RepositoryHora = new RepositoryHora();
        public IEnumerable<Hora> GetAllHoras()
        {
            return _RepositoryHora.GetAllHoras();
        }
    }
}

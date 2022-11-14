using Infraestructure.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryHora
    {
        IEnumerable<Hora> GetAllHoras();
    }
}

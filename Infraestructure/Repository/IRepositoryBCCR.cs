using Infraestructure.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryBCCR
    {
        Dolar GetDolar(string pCompraoVenta);

        double GetValueDollarToColones();
    }
}

using Infraestructure.Models.Service;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceBCCR : IServiceBCCR
    {
        private IRepositoryBCCR _RepositoryBCCR = new RepositoryBCCR();
        public Dolar GetDolar(string pCompraoVenta)
        {
            return _RepositoryBCCR.GetDolar(pCompraoVenta);
        }

        public double GetValueDollarToColones()
        {
            return _RepositoryBCCR.GetValueDollarToColones();
        }
    }
}

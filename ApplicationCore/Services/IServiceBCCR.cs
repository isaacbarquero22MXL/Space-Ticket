using Infraestructure.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceBCCR
    {
        Dolar GetDolar(string pCompraoVenta);

        double GetValueDollarToColones();
    }
}

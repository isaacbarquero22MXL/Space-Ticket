using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceFactura
    {
        IEnumerable<Factura> GetFactura();
        Factura GetFacturaByID(string id);
        void DeleteFactura(string id);
        Factura Save(Factura Factura);
        bool Existe(string id);

        string GenerateFacturaID();
    }
}

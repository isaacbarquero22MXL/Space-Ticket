using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryFacturaDetalle
    {
        IEnumerable<FacturaDetalle> GetAllFacturaDetalle();

        IEnumerable<FacturaDetalle> GetAllFacturaDetalleByFactura(string IDFactura);
        FacturaDetalle GetFacturaDetalleByID(int id);
        void DeleteFacturaDetalle(int id);
        FacturaDetalle Save(FacturaDetalle FacturaDetalle);
        bool Existe(int id);

    }
}

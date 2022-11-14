using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceFacturaDetalle : IServiceFacturaDetalle
    {
        private IRepositoryFacturaDetalle _RepositoryFacturaDetalle = new RepositoryFacturaDetalle();

        public void DeleteFacturaDetalle(int id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(int id)
        {
            return _RepositoryFacturaDetalle.Existe(id);
        }

        public IEnumerable<FacturaDetalle> GetAllFacturaDetalle()
        {
            return _RepositoryFacturaDetalle.GetAllFacturaDetalle();
        }

        public FacturaDetalle GetFacturaDetalleByID(int id)
        {
            return _RepositoryFacturaDetalle.GetFacturaDetalleByID(id);
        }

        public FacturaDetalle Save(FacturaDetalle FacturaDetalle)
        {
            return _RepositoryFacturaDetalle.Save(FacturaDetalle);
        }

        public IEnumerable<FacturaDetalle> GetAllFacturaDetalleByFactura(string IDFactura)
        {
            return _RepositoryFacturaDetalle.GetAllFacturaDetalleByFactura(IDFactura);
        }
    }
}

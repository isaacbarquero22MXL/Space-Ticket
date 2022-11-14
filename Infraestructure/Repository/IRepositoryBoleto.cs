using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryBoleto
    {
        IEnumerable<Boleto> GetBoleto();
        Boleto GetBoletoByID(string id);
        void DeleteBoleto(string id);
        Boleto Save(Boleto Boleto);
        bool Existe(string id);
    }
}

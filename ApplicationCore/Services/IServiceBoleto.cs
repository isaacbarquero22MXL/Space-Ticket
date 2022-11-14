using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceBoleto
    {
        IEnumerable<Boleto> GetBoleto();
        Boleto GetBoletoByID(string id);
        void DeleteBoleto(string id);
        Boleto Save(Boleto Boleto);
        bool Existe(string id);

        string GenerateBoletoID();
    }
}

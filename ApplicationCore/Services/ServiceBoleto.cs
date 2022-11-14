using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceBoleto : IServiceBoleto
    {
        IRepositoryBoleto _RepositoryBoleto = new RepositoryBoleto();
        private static int Consecutivo = 0;
        private static string IDBoleto;

        public void DeleteBoleto(string id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(string id)
        {
            return _RepositoryBoleto.Existe(id);
        }

        public IEnumerable<Boleto> GetBoleto()
        {
            return _RepositoryBoleto.GetBoleto();
        }

        public Boleto GetBoletoByID(string id)
        {
            return _RepositoryBoleto.GetBoletoByID(id);
        }

        public Boleto Save(Boleto Boleto)
        {
            return _RepositoryBoleto.Save(Boleto);
        }

        public string GenerateBoletoID()
        {
            GenerateID(); // Se genera el ID y  se almacena en la variable privada
            return IDBoleto;
        }

        public void GenerateID()
        {
            string ID = "B-" + ++Consecutivo;
            IDBoleto = ID;
            if (Existe(ID))
                GenerateID();
        }
    }
}

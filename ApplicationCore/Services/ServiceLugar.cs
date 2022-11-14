using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceLugar : IServiceLugar
    {
        private IRepositoryLugar _RepositoryLugar = new  RepositoryLugar(); 
        public void DeleteLugar(int id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(string codigo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lugar> GetLugar()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lugar> GetLugarByEventZone(string IDEvento, int IDZona)
        {
            return _RepositoryLugar.GetLugarByEventZone(IDEvento, IDZona);
        }

        public Lugar GetLugarByID(int id)
        {
            return _RepositoryLugar.GetLugarByID(id);
        }

        public Lugar Save(Lugar Lugar)
        {
           return _RepositoryLugar.Save(Lugar);
        }
    }
}

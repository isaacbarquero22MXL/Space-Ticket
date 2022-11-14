using Infraestructure.Models.Catalogo;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceZona : IServiceZona
    {
        private IRepositoryZona _RepositoryZona = new RepositoryZona();
        private IRepositoryLugar _RepositoryLugar= new RepositoryLugar();

        public void DeleteZona(int id)
        {
            _RepositoryZona.DeleteZona(id);
        }

        public bool Existe(int codigo)
        {
            return _RepositoryZona.Existe(codigo); 
        }

        public IEnumerable<Zona> GetZona()
        {
            return _RepositoryZona.GetZona();
        }

        public Zona GetZonaByID(int id)
        {
            return _RepositoryZona.GetZonaByID(id);
        }

        public IEnumerable<Zona> GetZonasByIDLugarEvento(int id)
        {
            return _RepositoryZona.GetZonasByIDLugarEvento(id);
        }

        public Zona Save(Zona Zona)
        {
           return _RepositoryZona.Save(Zona);
        }

        public void UpdateLugarByZona(Zona zona)
        {
            IEnumerable<Lugar> listaLugar = _RepositoryLugar.GetLugar().Where(x => x.IDZona == zona.ID).ToList();
        }
    }
}

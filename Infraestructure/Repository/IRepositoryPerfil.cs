using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryPerfil
    {
        IEnumerable<Perfil> GetPerfil();

        Perfil GetPerfilByID(int ID);

        void DeletePerfil(int id);
        Perfil Save(Perfil Perfil);
    }
}

using Infraestructure.Models.Catalogo;
using Infraestructure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryPerfil : IRepositoryPerfil
    {
        public void DeletePerfil(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Perfil> GetPerfil()
        {
            try
            {
                IEnumerable<Perfil> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Perfil.ToList();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Perfil GetPerfilByID(int ID)
        {
            Perfil perfil = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    perfil = ctx.Perfil.Find(ID);
                    //EmpleadoPerfil = ctx.EmpleadoPerfil.Where(x => x.IdEmpleadoPerfil == id).FirstOrDefault();
                }
                return perfil;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

       

        public Perfil Save(Perfil Perfil)
        {
            throw new NotImplementedException();
        }
    }
}

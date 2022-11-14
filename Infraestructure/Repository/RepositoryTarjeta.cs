using Infraestructure.Models.Catalogo;
using Infraestructure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTarjeta : IRepositoryTarjeta
    {
        public void DeleteTipoTarjeta(int id)
        {
            int returno;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoTarjeta TipoTarjeta = new TipoTarjeta()
                    {
                        ID = id
                    };
                    Log.Info("Se ingresa a eliminar la TipoTarjeta número " + TipoTarjeta.ID);
                    ctx.Entry(TipoTarjeta).State = EntityState.Deleted;
                    returno = ctx.SaveChanges();
                }
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

        public IEnumerable<TipoTarjeta> GetTipoTarjeta()
        {
            try
            {
                IEnumerable<TipoTarjeta> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false; 
                    lista = ctx.TipoTarjeta.Include(x => x.Tarjeta).
                        Where(x => x.Estado.Equals(TypeEstado.ACTIVO.ToString())).
                        ToList();
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

        public TipoTarjeta GetTipoTarjetaByID(int id)
        {
            TipoTarjeta TipoTarjeta = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoTarjeta = ctx.TipoTarjeta.Find(id);
                    //TipoTarjeta = ctx.TipoTarjeta.Where(x => x.IdTipoTarjeta == id).FirstOrDefault();
                }
                return TipoTarjeta;
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

        public TipoTarjeta Save(TipoTarjeta TipoTarjeta)
        {
            int retorno = 0;
            TipoTarjeta oTipoTarjeta = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipoTarjeta = GetTipoTarjetaByID(TipoTarjeta.ID);
                    if (oTipoTarjeta == null)
                    {
                        ctx.TipoTarjeta.Add(TipoTarjeta);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(TipoTarjeta).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oTipoTarjeta = GetTipoTarjetaByID(TipoTarjeta.ID);

                return oTipoTarjeta;
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
    }
}

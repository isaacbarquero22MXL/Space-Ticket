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
    public class RepositoryTipoEvento : IRepositoryTipoEvento
    {
        public void DeleteTipoEvento(int id)
        {
            int returno;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoEvento tipoEvento = new TipoEvento()
                    {
                        ID = id
                    };
                    Log.Info("Se ingresa a eliminar la TipoEvento número " + tipoEvento.ID);
                    ctx.Entry(tipoEvento).State = EntityState.Deleted;
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

        public IEnumerable<TipoEvento> GetTipoEvento()
        {
            try
            {
                IEnumerable<TipoEvento> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.TipoEvento.Where(x => x.Estado.Equals(TypeEstado.ACTIVO.ToString())).
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

        public TipoEvento GetTipoEventoByID(int id)
        {
            TipoEvento TipoEvento = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoEvento = ctx.TipoEvento.Find(id);
                    //TipoEvento = ctx.TipoEvento.Where(x => x.IdTipoEvento == id).FirstOrDefault();
                }
                return TipoEvento;
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

        public TipoEvento Save(TipoEvento TipoEvento)
        {
            int retorno = 0;
            TipoEvento oTipoEvento = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipoEvento = GetTipoEventoByID(TipoEvento.ID);
                    if (oTipoEvento == null)
                    {
                        ctx.TipoEvento.Add(TipoEvento);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(TipoEvento).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oTipoEvento = GetTipoEventoByID(TipoEvento.ID);

                return oTipoEvento;
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

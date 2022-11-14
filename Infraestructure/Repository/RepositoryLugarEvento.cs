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
    public class RepositoryLugarEvento : IRepositoryLugarEvento
    {
        public void DeleteLugarEvento(int id)
        {
            int returno;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    LugarEvento lugarEvento = new LugarEvento()
                    {
                        ID = id
                    };
                    Log.Info("Se ingresa a eliminar la LugarEvento número " + lugarEvento.ID);
                    ctx.Entry(lugarEvento).State = EntityState.Deleted;
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

        public IEnumerable<LugarEvento> GetLugarEvento()
        {
            try
            {
                IEnumerable<LugarEvento> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.LugarEvento.Where(x => x.Estado.Equals(TypeEstado.ACTIVO.ToString())).
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

        public LugarEvento GetLugarEventoByID(int id)
        {
            LugarEvento LugarEvento = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    LugarEvento = ctx.LugarEvento.Find(id);
                    //LugarEvento = ctx.LugarEvento.Where(x => x.IdLugarEvento == id).FirstOrDefault();
                }
                return LugarEvento;
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

        public LugarEvento Save(LugarEvento LugarEvento)
        {
            int retorno = 0;
            LugarEvento oLugarEvento = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oLugarEvento = GetLugarEventoByID(LugarEvento.ID);
                    if (oLugarEvento == null)
                    {
                        ctx.LugarEvento.Add(LugarEvento);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(LugarEvento).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oLugarEvento = GetLugarEventoByID(LugarEvento.ID);

                return oLugarEvento;
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

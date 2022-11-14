using Infraestructure.Models.Catalogo;
using Infraestructure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Infraestructure.Repository
{
    public class RepositoryEvento : IRepositoryEvento
    {
        public void DeleteEvento(string id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(string codigo)
        {
            bool flag = false;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Evento evento = ctx.Evento.Find(codigo);
                    if (evento != null)
                        flag = true;
                }

                return flag;
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

        public IEnumerable<Evento> GetAllEvents()
        {
            IRepositoryLugarEvento repositoryLugarEvento = new RepositoryLugarEvento();
            IRepositoryTipoEvento repositoryTipoEvento = new RepositoryTipoEvento();
            try
            {
                IEnumerable<Evento> lista = new List<Evento>();
                string sql =
                   string.Format("select * from Evento");
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lista = ctx.Evento.SqlQuery(sql).ToList();

                    foreach (var item in lista)
                    {
                        item.LugarEventoObject = repositoryLugarEvento.GetLugarEventoByID(item.LugarEvento);
                        item.TipoEventoObject = repositoryTipoEvento.GetTipoEventoByID(item.TipoEvento);
                    }
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

        public Evento GetEventByID(string ID)
        {
            try
            {
                Evento evento = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    evento = ctx.Evento.Include(x => x.TipoEventoObject).
                        Include(x => x.LugarEventoObject).
                        Where(e => e.ID.Equals(ID)).FirstOrDefault();
                }

                return evento;
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

        public Evento Save(Evento evento)
        {
            int retorno = 0;
            Evento oEvento = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEvento = GetEventByID(evento.ID);
                    if (oEvento == null)
                    {
                        ctx.Evento.Add(evento);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(evento).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oEvento = GetEventByID(evento.ID);

                return oEvento;
            }
            catch (DbEntityValidationException e)
            {
                string mensaje = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    mensaje += ("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        mensaje += ("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Log.Error(e, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
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

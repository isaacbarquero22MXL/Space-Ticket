using Infraestructure.Models.Catalogo;
using Infraestructure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryFacturaDetalle : IRepositoryFacturaDetalle
    {
        public void DeleteFacturaDetalle(int id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(int codigo)
        {
            bool flag = false;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    FacturaDetalle FacturaDetalle = ctx.FacturaDetalle.Find(codigo);
                    if (FacturaDetalle != null)
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

        public IEnumerable<FacturaDetalle> GetAllFacturaDetalle()
        {
            try
            {
                IEnumerable<FacturaDetalle> lista = new List<FacturaDetalle>();
                string sql =
                   string.Format("select * from FacturaDetalle");
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lista = ctx.FacturaDetalle.SqlQuery(sql).ToList();
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

        public FacturaDetalle GetFacturaDetalleByID(int ID)
        {
            try
            {
                FacturaDetalle FacturaDetalle = null;

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    FacturaDetalle = ctx.FacturaDetalle.
                        Where(e => e.ID == ID).FirstOrDefault();
                }

                return FacturaDetalle;
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

        public FacturaDetalle Save(FacturaDetalle FacturaDetalle)
        {
            int retorno = 0;
            FacturaDetalle oFacturaDetalle = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oFacturaDetalle = GetFacturaDetalleByID(FacturaDetalle.ID);
                    if (oFacturaDetalle == null)
                    {
                        ctx.FacturaDetalle.Add(FacturaDetalle);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(FacturaDetalle).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oFacturaDetalle = GetFacturaDetalleByID(FacturaDetalle.ID);

                return oFacturaDetalle;
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

        public IEnumerable<FacturaDetalle> GetAllFacturaDetalleByFactura(string IDFactura)
        {
            try
            {
                IEnumerable<FacturaDetalle> lista = new List<FacturaDetalle>();
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    lista = ctx.FacturaDetalle.Where(x => x.IDFactura.Equals(IDFactura)).ToList();
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
    }
}

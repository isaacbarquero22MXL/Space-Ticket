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
    public class RepositoryEmpleadoTelefono : IRepositoryEmpleadoTelefono
    {
        public void DeleteEmpleadoTelefono(int id)
        {
            int returno;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    EmpleadoTelefono EmpleadoTelefono = new EmpleadoTelefono()
                    {
                        ID = id
                    };
                    Log.Info("Se ingresa a eliminar la EmpleadoTelefono número " + EmpleadoTelefono.ID);
                    ctx.Entry(EmpleadoTelefono).State = EntityState.Deleted;
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

        public IEnumerable<EmpleadoTelefono> GetEmpleadoTelefono()
        {
            try
            {
                IEnumerable<EmpleadoTelefono> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EmpleadoTelefono.ToList();
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

        public IEnumerable<EmpleadoTelefono> GetEmpleadoTelefonoByIDEmpleado(string IDEmpleado)
        {
            IEnumerable<EmpleadoTelefono> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EmpleadoTelefono.Where(x => x.IDEmpleado.Equals(IDEmpleado)).ToList();
                    //EmpleadoTelefono = ctx.EmpleadoTelefono.Where(x => x.IdEmpleadoTelefono == id).FirstOrDefault();
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

        public EmpleadoTelefono GetEmpleadoTelefonoByID(int ID)
        {
            EmpleadoTelefono empleado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    empleado = ctx.EmpleadoTelefono.Find(ID);
                    //EmpleadoTelefono = ctx.EmpleadoTelefono.Where(x => x.IdEmpleadoTelefono == id).FirstOrDefault();
                }
                return empleado;
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

        public EmpleadoTelefono Save(EmpleadoTelefono EmpleadoTelefono)
        {
            int retorno = 0;
            EmpleadoTelefono oEmpleadoTelefono = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEmpleadoTelefono = GetEmpleadoTelefonoByID(EmpleadoTelefono.ID);
                    if (oEmpleadoTelefono == null)
                    {
                        ctx.EmpleadoTelefono.Add(EmpleadoTelefono);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(EmpleadoTelefono).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oEmpleadoTelefono = GetEmpleadoTelefonoByID(EmpleadoTelefono.ID);

                return oEmpleadoTelefono;
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

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
    public class RepositoryEmpleadoPerfil : IRepositoryEmpleadoPerfil
    {
        public void DeleteEmpleadoPerfil(int id)
        {
            int returno;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    EmpleadoPerfil EmpleadoPerfil = new EmpleadoPerfil()
                    {
                        ID = id
                    };
                    Log.Info("Se ingresa a eliminar la EmpleadoPerfil número " + EmpleadoPerfil.ID);
                    ctx.Entry(EmpleadoPerfil).State = EntityState.Deleted;
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

        public IEnumerable<EmpleadoPerfil> GetEmpleadoPerfil()
        {
            try
            {
                IEnumerable<EmpleadoPerfil> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EmpleadoPerfil.ToList();
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

        public IEnumerable<EmpleadoPerfil> GetEmpleadoPerfilByIDEmpleado(string IDEmpleado)
        {
            IEnumerable<EmpleadoPerfil> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EmpleadoPerfil.Where(x => x.IDEmpleado.Equals(IDEmpleado)).ToList();
                    //EmpleadoPerfil = ctx.EmpleadoPerfil.Where(x => x.IdEmpleadoPerfil == id).FirstOrDefault();
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

        public EmpleadoPerfil GetEmpleadoPerfilByID(int ID)
        {
            EmpleadoPerfil empleado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    empleado = ctx.EmpleadoPerfil.Find(ID);
                    //EmpleadoPerfil = ctx.EmpleadoPerfil.Where(x => x.IdEmpleadoPerfil == id).FirstOrDefault();
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

        public EmpleadoPerfil GetEmpleadoPerfilByIDEmpleado(int IDPerfil, string IDEmpleado)
        {
            EmpleadoPerfil perfil = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    perfil = ctx.EmpleadoPerfil.Where(x => x.IDPerfil == IDPerfil && x.IDEmpleado.Equals(IDEmpleado)).FirstOrDefault();
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

        public EmpleadoPerfil Save(EmpleadoPerfil EmpleadoPerfil)
        {
            int retorno = 0;
            EmpleadoPerfil oEmpleadoPerfil = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // se valida que el usuario no tenga registrado el mismo perfil, si es asi simplemente se actualiza
                    oEmpleadoPerfil = GetEmpleadoPerfilByIDEmpleado(EmpleadoPerfil.IDPerfil, EmpleadoPerfil.IDEmpleado);
                    if (oEmpleadoPerfil == null)
                    {
                        ctx.EmpleadoPerfil.Add(EmpleadoPerfil);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        EmpleadoPerfil = oEmpleadoPerfil;
                        ctx.Entry(EmpleadoPerfil).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oEmpleadoPerfil = GetEmpleadoPerfilByID(EmpleadoPerfil.ID);

                return oEmpleadoPerfil;
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

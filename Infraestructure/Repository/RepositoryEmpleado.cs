using Infraestructure.Models.Catalogo;
using Infraestructure.Models.DataModel;
using Infraestructure.Models.MasterClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryEmpleado : IRepositoryEmpleado 
    {
        public void DeleteEmpleado(string id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Empleado Empleado = new Empleado()
                    {
                        Cedula = id
                    };
                    ctx.Entry(Empleado).State = EntityState.Deleted;
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

        public Empleado GetEmpleadoByID(string id)
        {
            Empleado Empleado = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Empleado = ctx.Empleado.
                              Where(p => p.Cedula == id).
                              FirstOrDefault<Empleado>();
                }

                return Empleado;
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
        public IEnumerable<Empleado> GetEmpleado()
        {
            try
            {

                IEnumerable<Empleado> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Empleado.ToList<Empleado>();
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
        public Empleado Login(string email, string password)
        {
            try
            {
                Empleado empleado= null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    empleado = ctx.Empleado.Include(e => e.EmpleadoPerfil)
                        .Where(x => x.Correo.Equals(email) && x.Contrasenna.Equals(password) 
                                 && x.Estado.Equals(TypeEstado.ACTIVO.ToString()))
                        .FirstOrDefault();
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

        public Empleado Save(Empleado Empleado)
        {
            int retorno = 0;
            Empleado oEmpleado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEmpleado = GetEmpleadoByID(Empleado.Cedula);
                    if (oEmpleado == null)
                    {
                        ctx.Empleado.Add(Empleado);
                    }
                    else
                    {
                        ctx.Entry(Empleado).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oEmpleado = GetEmpleadoByID(Empleado.Cedula);

                return oEmpleado;
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

        public string[] GetRolesForUser(string username)
        {

            List<EmpleadoPerfil> lista = new List<EmpleadoPerfil>();
            string[] roles = null;
            List<string> userRoles = new List<string>();
            try
            {
                using (MyContext db = new MyContext())
                {
                    lista = db.EmpleadoPerfil.Include(x => x.Empleado).Include(x => x.Perfil).Where(x=> x.IDEmpleado.Equals(username)).ToList();
                    userRoles = lista.Select(r => r.Perfil.Descripcion).ToList();
                }

                if (lista != null)
                {
                    roles = userRoles.ToArray();
                }
                else
                {
                    roles = new string[] { "Ninguno" };
                }
                return roles;
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

        public bool Existe(string IDEmpleado)
        {
            bool existe = false;
            Empleado empleado = GetEmpleadoByID(IDEmpleado);
            if (empleado != null)
                existe = true;
            return existe;
        }
    }
}

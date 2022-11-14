using Infraestructure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models.Catalogo;

namespace Infraestructure.Repository
{
    public class RepositoryCliente : IRepositoryCliente
    {
        public void DeleteCliente(string id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Cliente Cliente = new Cliente()
                    {
                        Cedula = id
                    };
                    ctx.Entry(Cliente).State = EntityState.Deleted;
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

        public Cliente GetClienteByID(string id)
        {
            Cliente Cliente = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Cliente = ctx.Cliente.
                              Where(p => p.Cedula == id).
                              FirstOrDefault<Cliente>();
                }

                return Cliente;
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
        public IEnumerable<Cliente> GetCliente()
        {
            try
            {

                IEnumerable<Cliente> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Cliente.ToList();
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
        public Cliente Login(string email, string password)
        {
            try
            {
                Cliente Cliente = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Cliente = ctx.Cliente
                        .Where(x => x.Correo.Equals(email)
                        && x.Contrasenna.Equals(password) && x.Estado.Equals(TypeEstado.ACTIVO.ToString()))
                        .FirstOrDefault();
                }
                return Cliente;
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

        public Cliente Save(Cliente Cliente)
        {
            int retorno = 0;
            Cliente oCliente = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCliente = GetClienteByID(Cliente.Cedula);
                    if (oCliente == null)
                    {
                        ctx.Cliente.Add(Cliente);
                    }
                    else
                    {
                        ctx.Entry(Cliente).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oCliente = GetClienteByID(Cliente.Cedula);

                return oCliente;
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

        public string GetRolesForUser(string username)
        {
            string rol = "Ninguno";
            List<string> userRoles = new List<string>();
            try
            {
                if (GetClienteByID(username) != null)
                {
                    rol = "Cliente";
                }
 
                return rol;
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

        public bool Existe(String codigo)
        {
            bool existe = false;
            Cliente oCliente = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCliente = ctx.Cliente.
                        Where(x => x.CodigoCliente.Equals(codigo) || x.Cedula.Equals(codigo)).
                        FirstOrDefault();
                    if (oCliente != null)
                    {
                        existe = true;
                    }
                }

                return existe;
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

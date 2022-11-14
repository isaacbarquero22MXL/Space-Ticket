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
    public class RepositoryBoleto : IRepositoryBoleto
    {
        public void DeleteBoleto(string id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(string id)
        {
            bool existe = false;
            Boleto oBoleto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oBoleto = ctx.Boleto.
                        Where(x => x.ID.Equals(id)).FirstOrDefault();
                    if (oBoleto != null)
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

        public IEnumerable<Boleto> GetBoleto()
        {
            try
            {
                IEnumerable<Boleto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Boleto.
                        Include(x => x.Cliente).Include(x => x.Lugar).
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

        public Boleto GetBoletoByID(string id)
        {
            Boleto Boleto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Boleto = ctx.Boleto.Find(id);
                    //Boleto = ctx.Boleto.Where(x => x.IdBoleto == id).FirstOrDefault();
                }
                return Boleto;
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

        public Boleto Save(Boleto Boleto)
        {
            int retorno = 0;
            Boleto oBoleto = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oBoleto = GetBoletoByID(Boleto.ID);
                    if (oBoleto == null)
                    {
                        ctx.Boleto.Add(Boleto);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(Boleto).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oBoleto = GetBoletoByID(Boleto.ID);

                return oBoleto;
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

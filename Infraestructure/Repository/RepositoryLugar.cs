using Infraestructure.Models.Catalogo;
using Infraestructure.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Infraestructure.Repository
{
    public class RepositoryLugar : IRepositoryLugar
    {
        public void DeleteLugar(int id)
        {
            throw new NotImplementedException();
        }

        public bool Existe(int codigo)
        {
            bool flag = false;
            try
            {
                Lugar lugar = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lugar = ctx.Lugar.Find(codigo);
                    if (lugar != null)
                    {
                        flag = true;
                    }
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

        public IEnumerable<Lugar> GetLugarByEventZone(string IDEvento, int IDZona)
        {
            try
            {

                IEnumerable<Lugar> lista = null;
                string sql =
                    string.Format("select * from Lugar where IDEvento = '" + IDEvento + "' and IDZona = " + IDZona);
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Lugar.SqlQuery(sql).ToList<Lugar>();
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

        public IEnumerable<Lugar> GetLugar()
        {
            try
            {

                IEnumerable<Lugar> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Lugar.ToList<Lugar>();
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

        public Lugar GetLugarByID(int id)
        {
            try
            {
                Lugar lugar = null;
                string sql =
                   string.Format("select * from Lugar where ID = "+ id);
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lugar = ctx.Lugar.SqlQuery(sql).FirstOrDefault();
                }
                return lugar;
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

        public Lugar Save(Lugar lugar)
        {
            int retorno = 0;
            Lugar oLugar = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oLugar = GetLugarByID(lugar.ID);
                    if (oLugar == null)
                    {
                        ctx.Lugar.Add(lugar);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Entry(lugar).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oLugar = GetLugarByID(lugar.ID);

                return oLugar;
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

        public Lugar GetLugarByZoneRowAndSeat(string IDZona, string Row, string seat)
        {
            try
            {

                Lugar lugar = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lugar = ctx.Lugar.
                        Where(x => x.IDZona.Equals(IDZona) && x.Fila.Equals(Row) && x.NumeroAsiento.Equals(seat))
                       .FirstOrDefault();
                }
                return lugar;
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

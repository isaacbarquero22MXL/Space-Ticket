using Infraestructure.Models.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infraestructure.Repository
{
    public class RepositoryHora : IRepositoryHora
    {
        private readonly String PATH = HttpContext.Current.Request.MapPath("~/App_Data/hora.json");
        public IEnumerable<Hora> GetAllHoras()
        {

            List<Hora> lista = new List<Hora>();
            lista = JsonConvert.DeserializeObject<List<Hora>>(File.ReadAllText(PATH));
            return lista.OrderBy(p => p.ID).ToList();

        }
    }
}

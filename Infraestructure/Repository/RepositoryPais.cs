using Infraestructure.Models.Catalogo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryPais : IRepositoryPais
    {
        public IEnumerable<Pais> GetAllPaises()
        {
            List<Pais> lista = new List<Pais>();

            string responseBody = "";

            var url = $"http://country.io/names.json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null)
                        return null;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        responseBody = objReader.ReadToEnd();
                    }
                }
            }

            // Deserealización 
            JObject listaJ = JsonConvert.DeserializeObject<dynamic>(responseBody);
            foreach (var item in listaJ)
            {
                Pais Pais = new Pais()
                {
                    Id = item.Key.ToString(),
                    Name = item.Value.ToString()
                };
                lista.Add(Pais);
            }
            return lista;
        }
    }
}

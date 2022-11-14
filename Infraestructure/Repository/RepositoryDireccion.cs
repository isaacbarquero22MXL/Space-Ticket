using Infraestructure.Models.Direccion;
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
    public class RepositoryDireccion : IRepositoryDireccion
    {
        public IEnumerable<Canton> GetAllCantones()
        {
            IEnumerable<Provincia> listaP = GetAllProvicnias();
            List<Canton> lista = new List<Canton>();

            string responseBody = "";

            foreach (Provincia provincia in listaP)
            {
                var url = $"https://ubicaciones.paginasweb.cr/provincia/{provincia.ID}/cantones.json";
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
                // Deserealización 
                JObject listaJ = JsonConvert.DeserializeObject<dynamic>(responseBody);
                foreach (var item in listaJ)
                {
                    Canton canton = new Canton()
                    {
                        ID = int.Parse(item.Key),
                        Descripcion = item.Value.ToString(),
                        IDProvicnia = provincia.ID,

                    };
                    lista.Add(canton);
                }
            }
            return lista;
        }

        public IEnumerable<Distrito> GetAllDistritos()
        {
            IEnumerable<Provincia> listaP = GetAllProvicnias();
            IEnumerable<Canton> listaC = null;
            List<Distrito> lista = new List<Distrito>();

            foreach (Provincia provincia in listaP)
            {
                listaC = GetAllCantones().Where(c => c.IDProvicnia == provincia.ID);
                foreach (Canton canton in listaC)
                {
                    string responseBody = "";

                    var url = $"https://ubicaciones.paginasweb.cr/provincia/{provincia.ID}/canton/{canton.ID}/distritos.json";
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
                        Distrito distrito = new Distrito()
                        {
                            ID = int.Parse(item.Key),
                            Descripcion = item.Value.ToString()
                        };
                        lista.Add(distrito);
                    }
                }
            }
            return lista;
        }

        public IEnumerable<Provincia> GetAllProvicnias()
        {
            List<Provincia> lista = new List<Provincia>();

            string responseBody = "";

            var url = $"https://ubicaciones.paginasweb.cr/provincias.json";
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
                Provincia provincia = new Provincia()
                {
                    ID = int.Parse(item.Key),
                    Descripcion = item.Value.ToString()
                };
                lista.Add(provincia);
            }
            return lista;
        }
    }
}

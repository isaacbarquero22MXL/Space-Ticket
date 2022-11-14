using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models.Direccion
{
    public class Distrito
    {
        public int ID { get; set; }
        public String Descripcion { get; set; }
        public int IDCanton { get; set; }
        public int IDProvincia { get; set; }
    }
}

using Infraestructure.Models.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.ViewModel
{
    public class ViewModelEvento
    {
        public Evento Evento {get; set;}

        public String Month { get; set; }

        public String Day { get; set; }

        public String Year { get; set; }

        public bool isSold { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.ViewModel
{
    public class ViewModelBoletoDetalle
    {
        [Display(Name = "Boleto")]
        public string IDBoleto { get; set; }

        public string Fila { get; set; }

        public string Asiento { get; set; }

        [Display(Name = "Precio del Boleto")]
        public decimal PrecioBoleto { get; set; }
    }
}
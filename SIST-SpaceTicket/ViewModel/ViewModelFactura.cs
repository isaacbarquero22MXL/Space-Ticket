using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.ViewModel
{
    public class ViewModelFactura
    {
        [Display(Name = "No. Factura")]
        public string IDFactura { get; set; }

        [Display(Name = "Evento")]
        public string EventoName { get; set; }

        [Display(Name = "Zona")]
        public string ZonaName { get; set; }

        [Display(Name = "Fecha de pago")]
        public DateTime FechaPago { get; set; }

        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }

        [Display(Name = "Impuesto")]
        public decimal Impuesto { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }
    }
}
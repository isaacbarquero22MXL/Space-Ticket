using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.ViewModel
{
    public class ViewModelZona
    {
        [Required]
        [Display(Name ="Zona")]
        public int IDZona { get; set; }

        [Required]
        [Display(Name = "Evento")]
        public int IDEvento { get; set; }

        [Required]
        public int Estado { get; set; }

        [Required]
        [Display(Name = "Precio de Zona")]
        public decimal Precio { get; set; }
    }
}
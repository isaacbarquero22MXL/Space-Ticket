using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.ViewModel
{
    public class ViewModelLogin
    {
        [Required]
        [StringLength(150)]
        [DataType(DataType.EmailAddress, ErrorMessage = "El correo no tiene un formato válido.")]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        [StringLength(30)]
        public string Contrasenna { get; set; }
    }
}
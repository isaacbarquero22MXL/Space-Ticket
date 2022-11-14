using Infraestructure.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIST_SpaceTicket.ViewModel
{
    public class ViewModelCliente
    {
        [Required]
        [StringLength(12)]
        public string Cedula { get; set; }

        [Required]
        [StringLength(15)]
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [Required]
        [StringLength(15)]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        [Required]
        [StringLength(15)]
        public string Apellido2 { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Range(typeof(DateTime), "1/01/1912", "31/12/9999",
         ErrorMessage = "El valor para {0} debe estar entre {1} y {2}")]
        public DateTime FechaNac { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        [EmailValidation(ErrorMessage = "El correo no tiene un formato correcto.")]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        [StringLength(30)]
        public string Contrasenna { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Nacionalidad { get; set; }

        [StringLength(15)]
        public string Estado { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        public string ContrasennaConfirm { get; set; }
    }
}
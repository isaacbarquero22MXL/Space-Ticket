using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models.MasterClass
{
    public class Usuario
    {
        [Key]
        [StringLength(12)]
        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string Cedula { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string Nombre { get; set; }

        [StringLength(15)]
        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string Apellido1 { get; set; }

        [StringLength(15)]
        [Display(Name = "Segundo apellido")]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string Apellido2 { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "{0} es un campo requerido")]
        public string Correo { get; set; }

        [StringLength(30)]
        [Display(Name = "Constraseña")]
        public string Contrasenna { get; set; }

        [Required]
        [StringLength(15)]
        public string Estado { get; set; }
    }
}

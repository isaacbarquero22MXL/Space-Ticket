namespace Infraestructure.Models.Catalogo
{
    using Infraestructure.Validations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Empleado")]
    public partial class Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleado()
        {
            DireccionEmpleado = new HashSet<DireccionEmpleado>();
            EmpleadoPerfil = new HashSet<EmpleadoPerfil>();
            EmpleadoTelefono = new HashSet<EmpleadoTelefono>();
        }

        [Key]
        [StringLength(12)]
        [Display(Name = "Cédula")]
        [Required]
        public string Cedula { get; set; }

        [Required]
        [StringLength(15)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Primer apellido")]
        public string Apellido1 { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Segundo apellido")]
        public string Apellido2 { get; set; }

        [Required]
        [StringLength(150)]
        [EmailValidation(ErrorMessage = "El correo ingresado es inválido")]
        public string Correo { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        [MinLength(8)]
        public string Contrasenna { get; set; }

        [Required]
        [StringLength(15)]
        public string Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DireccionEmpleado> DireccionEmpleado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpleadoPerfil> EmpleadoPerfil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpleadoTelefono> EmpleadoTelefono { get; set; }
    }
}

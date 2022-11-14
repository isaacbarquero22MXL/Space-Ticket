namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmpleadoPerfil")]
    public partial class EmpleadoPerfil
    {
        public int ID { get; set; }

        [Required]
        [StringLength(12)]
        public string IDEmpleado { get; set; }

        [Display(Name = "Perfil")]
        [Required]
        public int IDPerfil { get; set; }


        [ForeignKey("IDEmpleado")]
        public virtual Empleado Empleado { get; set; }

        [ForeignKey("IDPerfil")]
        public virtual Perfil Perfil { get; set; }
    }
}

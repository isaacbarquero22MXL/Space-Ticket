namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmpleadoTelefono")]
    public partial class EmpleadoTelefono
    {
        public int ID { get; set; }

        [Required]
        [StringLength(12)]
        public string IDEmpleado { get; set; }

        [Required]
        [StringLength(18)]
        public string Telefono { get; set; }

        [ForeignKey("IDEmpleado")]
        public virtual Empleado Empleado { get; set; }
    }
}

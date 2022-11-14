namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DireccionEmpleado")]
    public partial class DireccionEmpleado
    {
        public int ID { get; set; }

        public int Provincia { get; set; }

        public int Canton { get; set; }

        public int Distrito { get; set; }

        [Required]
        [StringLength(12)]
        public string IDEmpleado { get; set; }

        public virtual Empleado Empleado { get; set; }
    }
}

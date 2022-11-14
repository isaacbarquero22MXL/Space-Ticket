namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FacturaDetalle")]
    public partial class FacturaDetalle
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string IDBoleto { get; set; }

        [Required]
        [StringLength(50)]
        public string IDFactura { get; set; }

        [ForeignKey("IDBoleto")]
        public virtual Boleto Boleto { get; set; }

        [ForeignKey("IDFactura")]
        public virtual Factura Factura { get; set; }
    }
}

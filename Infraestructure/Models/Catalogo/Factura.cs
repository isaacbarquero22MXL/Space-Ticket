namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Factura")]
    public partial class Factura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Factura()
        {
            FacturaDetalle = new HashSet<FacturaDetalle>();
        }

        [StringLength(50)]
        public string ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime FechaGeneracion { get; set; }

        public decimal SubTotal { get; set; }

        public decimal IVA { get; set; }

        public decimal Total { get; set; }

        [Column(TypeName = "xml")]
        [Required]
        public string FacturaXML { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }

        [NotMapped]
        public Evento Evento { get; set; }

        [NotMapped]
        public List<Boleto> ListaBoletos { get; set; }

        [NotMapped]
        public List<Lugar> ListaLugar{ get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacturaDetalle> FacturaDetalle { get; set; }
    }
}

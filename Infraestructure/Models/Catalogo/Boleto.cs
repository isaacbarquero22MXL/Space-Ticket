namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Boleto")]
    public partial class Boleto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Boleto()
        {
            FacturaDetalle = new HashSet<FacturaDetalle>();
        }

        [StringLength(10)]
        public string ID { get; set; }

        public DateTime FechaPago { get; set; }

        public int IDLugar { get; set; }

        [Required]
        [StringLength(15)]
        public string IDCliente { get; set; }

        [Required]
        [StringLength(12)]
        public string CedulaCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [ForeignKey("IDLugar")]
        public virtual Lugar Lugar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacturaDetalle> FacturaDetalle { get; set; }
    }
}

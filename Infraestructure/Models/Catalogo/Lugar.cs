namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lugar")]
    public partial class Lugar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lugar()
        {
            //Boleto = new HashSet<Boleto>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        public string NumeroAsiento { get; set; }

        [Required]
        [StringLength(2)]
        public string Fila { get; set; }

        public int IDZona { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string IDEvento { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Boleto> Boleto { get; set; }

        [ForeignKey("IDZona")]
        public virtual Zona Zona { get; set; }

        [ForeignKey("IDEvento")]
        public virtual Evento Evento { get; set; }
    }
}

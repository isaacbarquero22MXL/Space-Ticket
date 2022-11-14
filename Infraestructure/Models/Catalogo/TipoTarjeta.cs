namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoTarjeta")]
    public partial class TipoTarjeta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoTarjeta()
        {
            Tarjeta = new HashSet<Tarjeta>();
        }

        
        public int ID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "La descripción debe tener menos de {0} carácteres")]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
    }
}

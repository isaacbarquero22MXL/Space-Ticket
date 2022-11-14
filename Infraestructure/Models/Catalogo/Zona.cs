namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zona")]
    public partial class Zona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zona()
        {
            Lugar = new HashSet<Lugar>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Lugar de Evento")]
        public int IDLugarEvento { get; set; }

        [Display(Name = "Cantidad de Filas")]
        public int CantFilas { get; set; }

        [Display(Name = "Cantidad de Asientos por Fila")]
        public int CantAsientos { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Precio (Dólares)")]
        public decimal Precio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lugar> Lugar { get; set; }

        public virtual LugarEvento LugarEvento { get; set; }
    }
}

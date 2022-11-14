namespace Infraestructure.Models.Catalogo
{
    using Infraestructure.Utils.Validation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Evento")]
    public partial class Evento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evento()
        {
        }

        [Required]
        [StringLength(50)]
        public string ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }

        [Required]
        public string Imagen { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        [DateValidation(ErrorMessage = "La fecha del evento: {0} debe ser mayor a la fecha actual.")]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(10)]
        public string Hora { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        public int TipoEvento { get; set; }

        public int LugarEvento { get; set; }

        public virtual LugarEvento LugarEventoObject { get; set; }

        public virtual TipoEvento TipoEventoObject { get; set; }
    }
}

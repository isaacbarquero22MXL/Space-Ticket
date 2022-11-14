namespace Infraestructure.Models.Catalogo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tarjeta")]
    public partial class Tarjeta
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string Numero { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string MesVen { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string AnnoVen { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(4)]
        public string CVC { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TipoTarjeta { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(15)]
        public string IDCliente { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(12)]
        public string CedulaCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual TipoTarjeta TipoTarjeta1 { get; set; }
    }
}

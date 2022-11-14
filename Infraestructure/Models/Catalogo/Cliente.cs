namespace Infraestructure.Models.Catalogo
{
    using Infraestructure.Validations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Boleto = new HashSet<Boleto>();
            Tarjeta = new HashSet<Tarjeta>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string CodigoCliente { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(12)]
        [Required]
        public string Cedula { get; set; }

        [Required]
        [StringLength(15)]
        public string Nombre { get; set; }

        [Display(Name = "Primer apellido")]
        [Required]
        [StringLength(15)]
        public string Apellido1 { get; set; }

        [Display(Name = "Segundo apellido")]
        [Required]
        [StringLength(15)]
        public string Apellido2 { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Column(TypeName = "smalldatetime")]
        [Range(typeof(DateTime), "1/01/1912", "31/12/9999",
        ErrorMessage = "El valor para {0} debe estar entre {1} y {2}")]
        public DateTime FechaNac { get; set; }

        [Required]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        [EmailValidation(ErrorMessage = "{0}")]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        [MaxLength(int.MaxValue)]
        [MinLength(8)]
        public string Contrasenna { get; set; }

        [Required]
        [StringLength(30)]
        public string Nacionalidad { get; set; }

        [Required]
        [StringLength(15)]
        public string Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Boleto> Boleto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
    }
}

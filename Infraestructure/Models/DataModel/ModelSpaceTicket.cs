using Infraestructure.Models.Catalogo;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Infraestructure.Models.DataModel
{
    public partial class ModelSpaceTicket : DbContext
    {
        public ModelSpaceTicket()
            : base("name=ModelSpaceTicket")
        {
        }

        public virtual DbSet<Boleto> Boleto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DireccionEmpleado> DireccionEmpleado { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EmpleadoPerfil> EmpleadoPerfil { get; set; }
        public virtual DbSet<EmpleadoTelefono> EmpleadoTelefono { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public virtual DbSet<Lugar> Lugar { get; set; }
        public virtual DbSet<LugarEvento> LugarEvento { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<TipoEvento> TipoEvento { get; set; }
        public virtual DbSet<TipoTarjeta> TipoTarjeta { get; set; }
        public virtual DbSet<Zona> Zona { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boleto>()
                .HasMany(e => e.FacturaDetalle)
                .WithRequired(e => e.Boleto)
                .HasForeignKey(e => e.IDBoleto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Sexo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Contrasenna)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Boleto)
                .WithRequired(e => e.Cliente)
                .HasForeignKey(e => new { e.IDCliente, e.CedulaCliente })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Tarjeta)
                .WithRequired(e => e.Cliente)
                .HasForeignKey(e => new { e.IDCliente, e.CedulaCliente })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Contrasenna)
                .IsUnicode(false);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.DireccionEmpleado)
                .WithRequired(e => e.Empleado)
                .HasForeignKey(e => e.IDEmpleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.EmpleadoPerfil)
                .WithRequired(e => e.Empleado)
                .HasForeignKey(e => e.IDEmpleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.EmpleadoTelefono)
                .WithRequired(e => e.Empleado)
                .HasForeignKey(e => e.IDEmpleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Evento>();

            modelBuilder.Entity<Factura>()
                .HasMany(e => e.FacturaDetalle)
                .WithRequired(e => e.Factura)
                .HasForeignKey(e => e.IDFactura)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lugar>();

            modelBuilder.Entity<LugarEvento>()
                .HasMany(e => e.Evento)
                .WithRequired(e => e.LugarEventoObject)
                .HasForeignKey(e => e.LugarEvento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LugarEvento>()
                .HasMany(e => e.Zona)
                .WithRequired(e => e.LugarEvento)
                .HasForeignKey(e => e.IDLugarEvento)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Perfil>()
            //    .HasMany(e => e.EmpleadoPerfil)
            //    .WithRequired(e => e.Perfil)
            //    .HasForeignKey(e => e.IDPerfil)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoEvento>()
                .HasMany(e => e.Evento)
                .WithRequired(e => e.TipoEventoObject)
                .HasForeignKey(e => e.TipoEvento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoTarjeta>()
                .HasMany(e => e.Tarjeta)
                .WithRequired(e => e.TipoTarjeta1)
                .HasForeignKey(e => e.TipoTarjeta)
                .WillCascadeOnDelete(false);                

            modelBuilder.Entity<Zona>()
                .HasMany(e => e.Lugar)
                .WithRequired(e => e.Zona)
                .HasForeignKey(e => e.IDZona)
                .WillCascadeOnDelete(false);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ApiPresupuesto.Models
{
    public partial class PresupuestosModel : DbContext
    {
        public PresupuestosModel()
            : base("name=ModeloPresupuestos")
        {
        }

        public virtual DbSet<CategoriaPresupuesto> CategoriaPresupuesto { get; set; }
        public virtual DbSet<Concepto_Presupuesto> Concepto_Presupuesto { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Periodos> Periodos { get; set; }
        public virtual DbSet<Presupuesto_Detalle> Presupuesto_Detalle { get; set; }
        public virtual DbSet<Presupuesto_Header> Presupuesto_Header { get; set; }
        public virtual DbSet<Tipo_Presupuesto> Tipo_Presupuesto { get; set; }
        public virtual DbSet<Campañas> Campañas { get; set; }
        public virtual DbSet<Premios> Premios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaPresupuesto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Concepto_Presupuesto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Concepto_Presupuesto>()
                .Property(e => e.NombreComercial)
                .IsUnicode(false);

            modelBuilder.Entity<Concepto_Presupuesto>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Departamento>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Presupuesto_Detalle>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Presupuesto_Detalle>()
                .Property(e => e.Monto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Presupuesto_Detalle>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Presupuesto_Detalle>()
                .Property(e => e.TipoPago)
                .IsUnicode(false);

            modelBuilder.Entity<Presupuesto_Detalle>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Presupuesto_Header>()
                .Property(e => e.Limite)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Presupuesto_Header>()
                .Property(e => e.Activo)
                .IsUnicode(false);

            modelBuilder.Entity<Presupuesto_Header>()
                .Property(e => e.MontoDisponible)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Tipo_Presupuesto>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Campañas>()
                .Property(e => e.NombreCampania)
                .IsUnicode(false);

            modelBuilder.Entity<Premios>()
                .Property(e => e.NombrePremio)
                .IsUnicode(false);

            modelBuilder.Entity<Premios>()
                .Property(e => e.Monto)
                .HasPrecision(19, 4);
        }
    }
}

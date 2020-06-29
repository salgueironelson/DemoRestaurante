using System;
using DemoRestaurante.Api.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoRestaurante.Api.Data
{
    public partial class DemoRestauranteContext : DbContext
    {
        public DemoRestauranteContext()
        {
        }

        public DemoRestauranteContext(DbContextOptions<DemoRestauranteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Plato> Plato { get; set; }
        public virtual DbSet<Restaurante> Restaurante { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:demonetcore001.database.windows.net,1433;Initial Catalog=DemoComida;Persist Security Info=False;User ID=demo;Password=Abi0511.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plato>(entity =>
            {
                entity.Property(e => e.PlatoId).HasColumnName("PlatoID");

                entity.Property(e => e.Calificacion).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Plato1)
                    .IsRequired()
                    .HasColumnName("Plato")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ratings).HasColumnName("ratings");

                entity.Property(e => e.RestauranteId).HasColumnName("RestauranteID");

                entity.Property(e => e.TipoPlato)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.Plato)
                    .HasForeignKey(d => d.RestauranteId)
                    .HasConstraintName("FK__Plato__Restauran__5EBF139D");
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.Property(e => e.RestauranteId).HasColumnName("RestauranteID");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

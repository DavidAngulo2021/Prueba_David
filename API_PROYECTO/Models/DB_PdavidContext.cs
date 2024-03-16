using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_PROYECTO.Models
{
    public partial class DB_PdavidContext : DbContext
    {
        public DB_PdavidContext()
        {
        }

        public DB_PdavidContext(DbContextOptions<DB_PdavidContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(50)
                    .HasColumnName("primer_apellido");

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(50)
                    .HasColumnName("primer_nombre");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(50)
                    .HasColumnName("segundo_apellido");

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(50)
                    .HasColumnName("segundo_nombre");

                entity.Property(e => e.Sueldo)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("sueldo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

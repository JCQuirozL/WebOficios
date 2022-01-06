using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebOficios.Models;

namespace WebOficios.Data
{
    public partial class oficiosContext : DbContext
    {
        public oficiosContext()
        {
        }

        public oficiosContext(DbContextOptions<oficiosContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public virtual DbSet<Direccion> Direcciones { get; set; } = null!;
        public virtual DbSet<Oficio> Oficios { get; set; } = null!;
        public virtual DbSet<TipoOficio> TipoOficios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=T1\\SQLEXPRESS;Initial catalog=oficios;User=CXP;Password=20033029Jcq$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasKey(e => e.IdDireccion);

                entity.ToTable("direccion");

                entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Oficio>(entity =>
            {
                entity.HasKey(e => e.IdOficio);

                entity.ToTable("oficio");

                entity.Property(e => e.IdOficio).HasColumnName("id_oficio");

                entity.Property(e => e.Asunto)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("asunto");

                entity.Property(e => e.Capturo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("capturo");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.FolioSolicitud)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("folio_solicitud");

                entity.Property(e => e.IdDireccion).HasColumnName("id_direccion");

                entity.Property(e => e.IdTipo).HasColumnName("id_tipo");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.NOficio)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("n_oficio");

                entity.Property(e => e.PdfArchivo).HasColumnName("pdf_archivo");

                entity.Property(e => e.Realname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("realname");

                entity.HasOne(d => d.IdDireccionNavigation)
                    .WithMany(p => p.Oficios)
                    .HasForeignKey(d => d.IdDireccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_oficio_direccion");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Oficios)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_oficio_tipo_oficio");
            });

            modelBuilder.Entity<TipoOficio>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.ToTable("tipo_oficio");

                entity.Property(e => e.IdTipo).HasColumnName("id_tipo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

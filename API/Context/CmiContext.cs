using System;
using System.Collections.Generic;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public partial class CmiContext : DbContext
{
    public CmiContext()
    {
    }

    public CmiContext(DbContextOptions<CmiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClientesDetalle> ClientesDetalles { get; set; }

    public virtual DbSet<ClientesEliminado> ClientesEliminados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-I8V4A05R;DataBase=CMI;Trust Server Certificate=true;User Id=usuario1;Password=prueba123;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A743399FE7");

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<ClientesDetalle>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__Clientes__6E19D6FA579743F4");

            entity.ToTable("ClientesDetalle");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Direccion).HasMaxLength(255);

            entity.HasOne(d => d.Cliente).WithMany(p => p.ClientesDetalles)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente");
        });

        modelBuilder.Entity<ClientesEliminado>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.FechaEliminacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798DA98C819");

            entity.HasIndex(e => e.Usuario1, "UQ__Usuarios__E3237CF7B5B5118F").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

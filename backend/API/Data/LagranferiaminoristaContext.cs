using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public partial class LagranferiaminoristaContext : DbContext
{
    public LagranferiaminoristaContext()
    {
    }

    public LagranferiaminoristaContext(DbContextOptions<LagranferiaminoristaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vendedor> Vendedores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("categorias_pkey");

            entity.ToTable("categorias");

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdImagen).HasColumnName("id_imagen");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Ocultar).HasColumnName("ocultar");
            entity.Property(e => e.UrlImagen).HasColumnName("url_imagen");
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.IdEnvio).HasName("envios_pkey");

            entity.ToTable("envios");

            entity.Property(e => e.IdEnvio).HasColumnName("id_envio");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("pedidos_pkey");

            entity.ToTable("pedidos");

            entity.Property(e => e.IdPedido)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id_pedido");
            entity.Property(e => e.CantidadProductos).HasColumnName("cantidad_productos");
            entity.Property(e => e.Cliente).HasColumnName("cliente");
            entity.Property(e => e.CostoEnvio).HasColumnName("costo_envio");
            entity.Property(e => e.Detalle).HasColumnName("detalle");
            entity.Property(e => e.EnvioRetiro).HasColumnName("envio_retiro");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Vendedor).HasColumnName("vendedor");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.HasIndex(e => e.IdCategoria, "fki_fk_categoria");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdImagen).HasColumnName("id_imagen");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Ocultar).HasColumnName("ocultar");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.UrlImagen).HasColumnName("url_imagen");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categoria");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.IdVendedor).HasName("vendedores_pkey");

            entity.ToTable("vendedores");

            entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Ocultar).HasColumnName("ocultar");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

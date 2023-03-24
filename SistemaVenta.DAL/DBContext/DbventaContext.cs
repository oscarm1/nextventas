﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;

namespace SistemaVenta.DAL.DBContext;

public partial class DbventaContext : DbContext
{
    public DbventaContext()
    {
    }

    public DbventaContext(DbContextOptions<DbventaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<DetalleMovimiento> DetalleMovimiento { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Negocio> Negocios { get; set; }

    public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolMenu> RolMenus { get; set; }

    public virtual DbSet<TipoDocumentoMovimiento> TipoDocumentoMovimiento { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Movimiento> Movimiento { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Establishment> Establishments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<GeneralParams> GeneralParams { get; set; }
    public DbSet<Plan> Plans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C512DD09F");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Configuracion");

            entity.Property(e => e.Propiedad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("propiedad");
            entity.Property(e => e.Recurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("recurso");
            entity.Property(e => e.Valor)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<DetalleMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdDetalleMovimiento).HasName("PK__DetalleV__BFE2843FB3D3EFB5");

            entity.Property(e => e.IdDetalleMovimiento).HasColumnName("idDetalleMovimiento");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            // entity.Property(e => e.CategoriaProducto)
            //.HasMaxLength(100)
            //.IsUnicode(false)
            //.HasColumnName("categoriaProducto");
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcionProducto");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdMovimiento).HasColumnName("idMovimiento");
            //entity.Property(e => e.MarcaProducto)
            //.HasMaxLength(100)
            //.IsUnicode(false)
            //.HasColumnName("marcaProducto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleMovimiento)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_DetalleMovimiento_Producto");

            entity.HasOne(d => d.IdMovimientoNavigation).WithMany(p => p.DetalleMovimiento)
                .HasForeignKey(d => d.IdMovimiento)
                .HasConstraintName("FK__DetalleVe__idVen__440B1D61");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF483A6068BB7");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Controlador)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("controlador");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Icono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("icono");
            entity.Property(e => e.IdMenuPadre).HasColumnName("idMenuPadre");
            entity.Property(e => e.PaginaAccion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("paginaAccion");

            entity.HasOne(d => d.IdMenuPadreNavigation).WithMany(p => p.InverseIdMenuPadreNavigation)
                .HasForeignKey(d => d.IdMenuPadre)
                .HasConstraintName("FK__Menu__idMenuPadr__24927208");
        });

        modelBuilder.Entity<Negocio>(entity =>
        {
            entity.HasKey(e => e.IdNegocio).HasName("PK__Negocio__70E1E1076DE9A588");

            entity.ToTable("Negocio");

            entity.Property(e => e.IdNegocio)
                .ValueGeneratedNever()
                .HasColumnName("idNegocio");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreLogo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreLogo");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.PorcentajeImpuesto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("porcentajeImpuesto");
            entity.Property(e => e.SimboloMoneda)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("simboloMoneda");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UrlLogo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlLogo");
        });

        modelBuilder.Entity<NumeroCorrelativo>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCorrelativo).HasName("PK__NumeroCo__25FB547E65A40777");

            entity.ToTable("NumeroCorrelativo");

            entity.Property(e => e.IdNumeroCorrelativo).HasColumnName("idNumeroCorrelativo");
            entity.Property(e => e.CantidadDigitos).HasColumnName("cantidadDigitos");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("fechaActualizacion");
            entity.Property(e => e.Gestion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("gestion");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimoNumero");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A132797CBE6F");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.CodigoBarra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigoBarra");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.NombreImagen)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreImagen");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlImagen");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__idCate__36B12243");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
               .HasForeignKey(d => d.IdProveedor)
               .HasConstraintName("FK__Producto__idProv__36B12244");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F7648303C61");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.HasKey(e => e.IdRolMenu).HasName("PK__RolMenu__CD2045D88E5957FC");

            entity.ToTable("RolMenu");

            entity.Property(e => e.IdRolMenu).HasColumnName("idRolMenu");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK__RolMenu__idMenu__2C3393D0");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__RolMenu__idRol__2B3F6F97");
        });

        modelBuilder.Entity<TipoDocumentoMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumentoMovimiento).HasName("PK__TipoDocu__A9D59AEE8BEB6B2E");

            entity.Property(e => e.IdTipoDocumentoMovimiento).HasColumnName("idTipoDocumentoMovimiento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6ACC301A0");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreFoto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlFoto");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__idRol__300424B4");
        });
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedor__645723A6ACC301A0");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            //entity.Property(e => e.Clave)
            //    .HasMaxLength(100)
            //    .IsUnicode(false)
            //    .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            // entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            //entity.Property(e => e.NombreFoto)
            //    .HasMaxLength(100)
            //    .IsUnicode(false)
            //    .HasColumnName("nombreFoto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            //entity.Property(e => e.UrlFoto)
            //    .HasMaxLength(500)
            //    .IsUnicode(false)
            //    .HasColumnName("urlFoto");

            //entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
            //    .HasForeignKey(d => d.IdRol)
            //    .HasConstraintName("FK__Usuario__idRol__300424B4");
        });
        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimiento__077D56148B22AC5G");

            entity.Property(e => e.IdMovimiento).HasColumnName("idMovimiento");
            entity.Property(e => e.DocumentoCliente)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("documentoCliente");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdTipoDocumentoMovimiento).HasColumnName("idTipoDocumentoMovimiento");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.ImpuestoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("impuestoTotal");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombreCliente");
            entity.Property(e => e.NumeroMovimiento)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("numeroMovimiento");
            entity.Property(e => e.SubTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subTotal");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTipoDocumentoMovimientoNavigation).WithMany(p => p.Movimiento)
                .HasForeignKey(d => d.IdTipoDocumentoMovimiento)
                .HasConstraintName("FK__Movimiento__idTipoDoc__3F466844");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Movimiento)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Movimiento__idUsuario__403A8C7D");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Movimiento)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Movimiento__idProveedor__403A8C7D");

        });


        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Guest>()
            .HasKey(g => g.IdGuest);

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Book>()
            .HasKey(gr => gr.IdBook);

        modelBuilder.Entity<Book>()
            .HasOne(gr => gr.IdRoomNavigation)
            .WithMany(r => r.book)
            .HasForeignKey(gr => gr.IdRoom);

        modelBuilder.Entity<Book>()
            .HasOne(gr => gr.IdGuestNavigation)
            .WithMany(g => g.book)
            .HasForeignKey(gr => gr.IdGuest)
            .OnDelete(DeleteBehavior.NoAction);


        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Establishment>()
            .HasKey(e => e.IdEstablishment);

        modelBuilder.Entity<Establishment>()
            .HasMany(e => e.Rooms)
            .WithOne(r => r.IdEstablishmentNavigation)
            .HasForeignKey(r => r.IdEstablishment);

        modelBuilder.Entity<Establishment>()
            .HasOne(e => e.Company)
            .WithMany(c => c.Establishments)
            .HasForeignKey(e => e.IdCompany);

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<User>()
            .HasKey(u => u.IdUser);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.IdCompany);

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Room>()
            .HasKey(r => r.IdRoom);

        modelBuilder.Entity<Room>()
            .HasOne(r => r.IdEstablishmentNavigation)
            .WithMany(e => e.Rooms)
            .HasForeignKey(r => r.IdEstablishment);

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Subscription>()
            .HasKey(s => s.IdSubscription);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Company)
            .WithMany(c => c.Subscriptions)
            .HasForeignKey(s => s.IdCompany);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Plan)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(s => s.IdPlan);

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Company>()
            .HasKey(c => c.IdCompany);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Establishments)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.IdCompany);

        modelBuilder.Entity<Company>()
             .HasMany(c => c.Users)
             .WithOne(u => u.Company)
             .HasForeignKey(u => u.IdCompany);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Subscriptions)
            .WithOne(s => s.Company)
            .HasForeignKey(s => s.IdCompany);

        modelBuilder.Entity<Company>()
            .Property(c => c.CompanyNumber)
            .IsRequired();

        modelBuilder.Entity<Company>()
            .Property(c => c.CompanyName)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Company>()
            .Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Company>()
            .Property(c => c.PhoneNumber)
            .HasMaxLength(20);

        modelBuilder.Entity<Company>()
            .Property(c => c.CreationDate)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<Company>()
            .Property(c => c.ModificationDate)
            .HasDefaultValueSql("GETDATE()");

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Plan>()
            .HasKey(c => c.IdPlan);

        modelBuilder.Entity<Plan>()
            .HasMany(c => c.Subscriptions)
            .WithOne(e => e.Plan)
            .HasForeignKey(e => e.IdPlan);

        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

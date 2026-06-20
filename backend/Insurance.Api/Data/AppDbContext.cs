using Insurance.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();
    public DbSet<Cobertura> Coberturas => Set<Cobertura>();
    public DbSet<Poliza> Polizas => Set<Poliza>();
    public DbSet<PolizaCobertura> PolizaCoberturas => Set<PolizaCobertura>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Tbl_Clientes");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nombre).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Identificacion).HasMaxLength(30).IsRequired();
            entity.Property(x => x.Correo).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Telefono).HasMaxLength(30);
            entity.HasIndex(x => x.Identificacion).IsUnique();
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Tbl_Vehiculos");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Placa).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Marca).HasMaxLength(80).IsRequired();
            entity.Property(x => x.Modelo).HasMaxLength(80).IsRequired();
            entity.Property(x => x.ValorComercial).HasPrecision(18, 2);
            entity.HasIndex(x => x.Placa).IsUnique();
        });

        modelBuilder.Entity<Cobertura>(entity =>
        {
            entity.ToTable("Tbl_Coberturas");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Descripcion).HasMaxLength(250);
            entity.Property(x => x.MontoCobertura).HasPrecision(18, 2);
            entity.HasIndex(x => x.Nombre).IsUnique();
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.ToTable("Tbl_Polizas");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.NumeroPoliza).HasMaxLength(30).IsRequired();
            entity.Property(x => x.SumaAsegurada).HasPrecision(18, 2);
            entity.Property(x => x.PrimaTotal).HasPrecision(18, 2);
            entity.Property(x => x.Estado).HasMaxLength(30).IsRequired();

            entity.HasIndex(x => x.NumeroPoliza).IsUnique();

            entity.HasOne(x => x.Cliente)
                .WithMany(x => x.Polizas)
                .HasForeignKey(x => x.ClienteId);

            entity.HasOne(x => x.Vehiculo)
                .WithMany(x => x.Polizas)
                .HasForeignKey(x => x.VehiculoId);
        });

        modelBuilder.Entity<PolizaCobertura>(entity =>
        {
            entity.ToTable("Tbl_PolizaCoberturas");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.MontoAplicado).HasPrecision(18, 2);

            entity.HasIndex(x => new { x.PolizaId, x.CoberturaId }).IsUnique();

            entity.HasOne(x => x.Poliza)
                .WithMany(x => x.PolizaCoberturas)
                .HasForeignKey(x => x.PolizaId);

            entity.HasOne(x => x.Cobertura)
                .WithMany(x => x.PolizaCoberturas)
                .HasForeignKey(x => x.CoberturaId);
        });
    }
}

using AstroTrack.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AstroTrack.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Motorista> Motoristas { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Viagem> Viagens { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }
    public DbSet<UsuarioSistema> UsuariosSistema { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("AT_CLIENTES");
            entity.HasKey(e => e.IdCliente);
            entity.Property(e => e.IdCliente).UseIdentityColumn();
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasIndex(e => e.Cnpj).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Motorista>(entity =>
        {
            entity.ToTable("AT_MOTORISTAS");
            entity.HasKey(e => e.IdMotorista);
            entity.Property(e => e.IdMotorista).UseIdentityColumn();
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasIndex(e => e.Cpf).IsUnique();
            entity.HasIndex(e => e.Cnh).IsUnique();
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.ToTable("AT_VEICULOS");
            entity.HasKey(e => e.IdVeiculo);
            entity.Property(e => e.IdVeiculo).UseIdentityColumn();
            entity.Property(e => e.Status).HasConversion<string>();
            entity.HasIndex(e => e.Placa).IsUnique();
        });

        modelBuilder.Entity<Viagem>(entity =>
        {
            entity.ToTable("AT_VIAGENS");
            entity.HasKey(e => e.IdViagem);
            entity.Property(e => e.IdViagem).UseIdentityColumn();
            entity.Property(e => e.Status).HasConversion<string>();

            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Viagens)
                .HasForeignKey(e => e.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Motorista)
                .WithMany(m => m.Viagens)
                .HasForeignKey(e => e.IdMotorista)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Veiculo)
                .WithMany(v => v.Viagens)
                .HasForeignKey(e => e.IdVeiculo)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Checkpoint>(entity =>
        {
            entity.ToTable("AT_CHECKPOINTS");
            entity.HasKey(e => e.IdCheckpoint);
            entity.Property(e => e.IdCheckpoint).UseIdentityColumn();

            entity.Property(e => e.BotaoPanico)
                .HasColumnType("NUMBER(1)")
                .HasConversion(
                    v => v,
                    v => v);

            entity.Property(e => e.PortaAberta)
                .HasColumnType("NUMBER(1)")
                .HasConversion(
                    v => v,
                    v => v);

            entity.HasOne(e => e.Viagem)
                .WithMany(v => v.Checkpoints)
                .HasForeignKey(e => e.IdViagem)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
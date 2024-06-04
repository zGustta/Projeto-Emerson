using FatecSisMed.MedicoAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace FatecSisMed.MedicoAPI.Context.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    //fazemos o mapeamento do objeto relacional do nosso BD
    public DbSet<Convenio> Convenios { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<Medico> Medicos { get; set; }

    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Remedio> Remedios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Convenio>().HasKey(c => c.Id);
        modelBuilder.Entity<Convenio>().Property(c => c.Nome).HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Especialidade>().HasKey(e => e.Id);
        modelBuilder.Entity<Especialidade>().Property(e => e.Nome).HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Medico>().HasKey(m => m.Id);
        modelBuilder.Entity<Medico>().Property(m => m.Nome).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Medico>().Property(m => m.Email).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Medico>().Property(m => m.Telefone).HasMaxLength(20).IsRequired();
        modelBuilder.Entity<Medico>().Property(m => m.Email).HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Remedio>().HasKey(e => e.Id);
        modelBuilder.Entity<Remedio>().Property(e => e.Nome).HasMaxLength(100).IsRequired();

        modelBuilder.Entity<Marca>().HasKey(e => e.Id);
        modelBuilder.Entity<Marca>().Property(e => e.Nome).HasMaxLength(100).IsRequired();


        //relacionamentos
        modelBuilder.Entity<Convenio>().HasMany(c => c.Medicos).WithOne(m => m.Convenio)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Especialidade>().HasMany(e => e.Medicos).WithOne(m => m.Especialidade)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Marca>().HasMany(e => e.Remedios).WithOne(m => m.Marca)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);

    }

}
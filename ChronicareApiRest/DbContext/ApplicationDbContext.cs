using ChronicareApiRest.Entity;
using ChronicareApiRest.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<RiesgoPaciente> RiesgosPaciente { get; set; }
    public DbSet<Alerta> Alertas { get; set; }
    public DbSet<Registro> Registros { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Paciente>().ToTable("paciente", "chronicare");

        builder.Entity<Registro>()
            .HasOne(r => r.Paciente)
            .WithMany(p => p.Registros)
            .HasForeignKey(r => r.IdPaciente)
            .HasConstraintName("fk_registro_paciente");

        builder.Entity<Alerta>()
            .HasOne(a => a.Paciente)
            .WithMany(p => p.Alertas)
            .HasForeignKey(a => a.IdPaciente)
            .HasConstraintName("fk_alerta_paciente");

        builder.Entity<RiesgoPaciente>()
            .HasOne(r => r.Paciente)
            .WithMany(p => p.Riesgos)
            .HasForeignKey(r => r.IdPaciente)
            .HasConstraintName("fk_riesgo_paciente_paciente");
    }
}

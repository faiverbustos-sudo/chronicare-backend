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
    public DbSet<Medico> Medicos { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Registro>()
            .HasOne(r => r.Paciente)
            .WithMany(p => p.Registros)
            .HasForeignKey(r => r.IdPaciente)
            .HasConstraintName("registro_id_paciente_fkey");

        builder.Entity<Registro>()
            .HasOne(r => r.Medico)
            .WithMany(p => p.Registros)
            .HasForeignKey(r => r.IdMedico)
            .HasConstraintName("registro_id_medico_fkey");

        builder.Entity<Alerta>()
            .HasOne(a => a.Paciente)
            .WithMany(p => p.Alertas)
            .HasForeignKey(a => a.IdPaciente)
            .HasConstraintName("alerta_id_paciente_fkey");

        builder.Entity<RiesgoPaciente>()
            .HasOne(r => r.Paciente)
            .WithMany(p => p.Riesgos)
            .HasForeignKey(r => r.IdPaciente)
            .HasConstraintName("riesgo_paciente_id_paciente_fkey");
    }
}

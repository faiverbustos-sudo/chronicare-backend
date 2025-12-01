using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("tarea", Schema = "chronicare")]
public class Tarea
{
    [Key]
    [Column("id_tarea")]
    public Guid IdTarea { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Required]
    [Column("tipo_tarea")]
    [MaxLength(80)]
    public string TipoTarea { get; set; }

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("fecha_programada")]
    public DateTime? FechaProgramada { get; set; }

    [Column("completada")]
    public bool Completada { get; set; }

    [Column("id_alerta")]
    public Guid? IdAlerta { get; set; }

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }

    [Column("fecha_completada")]
    public DateTime? FechaCompletada { get; set; }

    public Paciente Paciente { get; set; }
    public Alerta? Alerta { get; set; }
}

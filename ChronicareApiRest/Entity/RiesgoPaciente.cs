using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("riesgo_paciente", Schema = "chronicare")]
public class RiesgoPaciente
{
    [Key]
    [Column("id_riesgo")]
    public Guid IdRiesgo { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Required]
    [Column("nivel_riesgo")]
    [MaxLength(10)]
    public string NivelRiesgo { get; set; }

    [Column("causa")]
    public string? Causa { get; set; }

    [Column("fecha_actualizacion")]
    public DateTime FechaActualizacion { get; set; }

    public Paciente Paciente { get; set; }

}

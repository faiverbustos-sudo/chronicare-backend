using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChronicareApiRest.Entity;

[Table("adherencia_medicamento", Schema = "chronicare")]
public class AdherenciaMedicamento
{
    [Key]
    [Column("id_adherencia")]
    public Guid IdAdherencia { get; set; }

    [Column("id_medicamento")]
    public Guid IdMedicamento { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; }

    [Column("tomado")]
    public bool Tomado { get; set; }

    [Column("observacion")]
    public string? Observacion { get; set; }

    public Medicamento Medicamento { get; set; }
    public Paciente Paciente { get; set; }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("medicamento", Schema = "chronicare")]
public class Medicamento
{
    [Key]
    [Column("id_medicamento")]
    public Guid IdMedicamento { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Required]
    [Column("nombre")]
    [MaxLength(200)]
    public string Nombre { get; set; }

    [Column("dosis")]
    [MaxLength(100)]
    public string? Dosis { get; set; }

    [Column("frecuencia")]
    [MaxLength(100)]
    public string? Frecuencia { get; set; }

    [Column("via")]
    [MaxLength(50)]
    public string? Via { get; set; }

    [Column("fecha_inicio")]
    public DateTime? FechaInicio { get; set; }

    [Column("fecha_fin")]
    public DateTime? FechaFin { get; set; }

    [Column("activo")]
    public bool Activo { get; set; }

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }

    public Paciente Paciente { get; set; }
}

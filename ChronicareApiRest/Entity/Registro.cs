using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("registro", Schema = "chronicare")]
public class Registro
{
    [Key]
    [Column("id_registro")]
    public Guid IdRegistro { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Column("id_medico")]
    public Guid? IdMedico { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; }

    [Required]
    [Column("tipo_registro")]
    [MaxLength(50)]
    public string TipoRegistro { get; set; }

    [Column("valor_numerico", TypeName = "numeric(12,4)")]
    public decimal? ValorNumerico { get; set; }

    [Column("valor_sistolica")]
    public int? ValorSistolica { get; set; }

    [Column("valor_diastolica")]
    public int? ValorDiastolica { get; set; }

    [Column("unidad")]
    [MaxLength(20)]
    public string? Unidad { get; set; }

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    // RELACIONES
    public Paciente Paciente { get; set; }
    public Medico? Medico { get; set; }
    public ICollection<Alerta> Alertas { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChronicareApiRest.Entity;

[Table("alerta", Schema = "chronicare")]
public class Alerta
{
    [Key]
    [Column("id_alerta")]
    public Guid IdAlerta { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Column("id_registro")]
    public Guid? IdRegistro { get; set; }

    [Required]
    [Column("tipo_alerta")]
    [MaxLength(80)]
    public string TipoAlerta { get; set; }

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Required]
    [Column("nivel_riesgo")]
    [MaxLength(10)]
    public string NivelRiesgo { get; set; }

    [Column("fecha_alerta")]
    public DateTime FechaAlerta { get; set; }

    [Required]
    [Column("estado")]
    [MaxLength(20)]
    public string Estado { get; set; }

    // RELACIONES
    public Paciente Paciente { get; set; }
    public Registro? Registro { get; set; }
    public ICollection<Tarea> Tareas { get; set; }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("evolucion_clinica", Schema = "chronicare")]
public class EvolucionClinica
{
    [Key]
    [Column("id_evolucion")]
    public Guid IdEvolucion { get; set; }

    [Column("id_medico")]
    public Guid? IdMedico { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Column("fecha_evolucion")]
    public DateTime FechaEvolucion { get; set; }

    [Column("nota")]
    public string? Nota { get; set; }

    [Column("origen")]
    [MaxLength(30)]
    public string? Origen { get; set; }

    [Column("datos_fhir")]
    public string? DatosFhir { get; set; } // JSONB → string

    public Paciente Paciente { get; set; }
    public Medico? Medico { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("mensaje", Schema = "chronicare")]
public class Mensaje
{
    [Key]
    [Column("id_mensaje")]
    public Guid IdMensaje { get; set; }

    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Column("id_medico")]
    public Guid? IdMedico { get; set; }

    [Required]
    [Column("emisor")]
    [MaxLength(20)]
    public string Emisor { get; set; }

    [Required]
    [Column("mensaje")]
    public string MensajeTexto { get; set; }

    [Column("fecha_envio")]
    public DateTime FechaEnvio { get; set; }

    [Column("visto")]
    public bool Visto { get; set; }

    public Paciente Paciente { get; set; }
    public Medico? Medico { get; set; }
}

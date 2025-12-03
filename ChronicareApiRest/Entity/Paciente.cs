using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("paciente", Schema = "chronicare")]
public class Paciente
{
    [Key]
    [Column("id_paciente")]
    public Guid IdPaciente { get; set; }

    [Column("documento")]
    [MaxLength(20)]
    public string? Documento { get; set; }

    [Column("tipo_documento")]
    [MaxLength(5)]
    public string? TipoDocumento { get; set; }

    [Column("nombre")]
    [Required]
    [MaxLength(200)]
    public string Nombre { get; set; }

    [Column("fecha_nacimiento")]
    public DateTime? FechaNacimiento { get; set; }

    [Column("genero")]
    [MaxLength(20)]
    public string? Genero { get; set; }

    [Column("telefono")]
    [MaxLength(30)]
    public string? Telefono { get; set; }

    [Column("email")]
    [MaxLength(200)]
    public string? Email { get; set; }

    [Column("direccion")]
    [MaxLength(250)]
    public string? Direccion { get; set; }

    [Column("eps")]
    [MaxLength(150)]
    public string? Eps { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; }

    [Column("estado")]
    [Required]
    [MaxLength(20)]
    public string Estado { get; set; }

    public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
    public ICollection<Registro> Registros { get; set; } = new List<Registro>();
    public ICollection<RiesgoPaciente> Riesgos { get; set; } = new List<RiesgoPaciente>();

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("medico", Schema = "chronicare")]
public class Medico
{
    [Key]
    [Column("id_medico")]
    public Guid IdMedico { get; set; }

    [Required]
    [Column("nombre")]
    [MaxLength(200)]
    public string Nombre { get; set; }

    [Column("registro_medico")]
    [MaxLength(50)]
    public string? RegistroMedico { get; set; }

    [Column("especialidad")]
    [MaxLength(100)]
    public string? Especialidad { get; set; }

    [Column("email")]
    [MaxLength(200)]
    public string? Email { get; set; }

    [Column("telefono")]
    [MaxLength(30)]
    public string? Telefono { get; set; }

    [Column("usuario_sistema")]
    [MaxLength(100)]
    public string? UsuarioSistema { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; }

    [Column("estado")]
    [Required]
    [MaxLength(20)]
    public string Estado { get; set; }

    public ICollection<Registro> Registros { get; set; } = new List<Registro>();
}

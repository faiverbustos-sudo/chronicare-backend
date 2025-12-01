using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChronicareApiRest.Entity;

[Table("auditoria", Schema = "chronicare")]
public class Auditoria
{
    [Key]
    [Column("id_auditoria")]
    public Guid IdAuditoria { get; set; }

    [Column("entidad")]
    [MaxLength(100)]
    public string? Entidad { get; set; }

    [Column("id_entidad")]
    public Guid? IdEntidad { get; set; }

    [Column("accion")]
    [MaxLength(50)]
    public string? Accion { get; set; }

    [Column("datos")]
    public string? Datos { get; set; } // JSONB → string

    [Column("usuario")]
    [MaxLength(100)]
    public string? Usuario { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; }
}

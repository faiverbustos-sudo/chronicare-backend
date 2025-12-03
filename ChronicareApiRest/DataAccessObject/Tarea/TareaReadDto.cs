using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChronicareApiRest.DataAccessObject.Tarea;

public class TareaReadDto
{
    public Guid IdTarea { get; set; }

    public Guid IdPaciente { get; set; }

    public string TipoTarea { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaProgramada { get; set; }

    public bool Completada { get; set; }

    public Guid? IdAlerta { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaCompletada { get; set; }
}

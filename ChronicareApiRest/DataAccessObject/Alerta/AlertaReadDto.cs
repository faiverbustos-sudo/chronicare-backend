namespace ChronicareApiRest.DataAccessObject.Alerta;

public class AlertaReadDto
{
    public Guid IdAlerta { get; set; }
    public Guid IdPaciente { get; set; }
    public Guid? IdRegistro { get; set; }
    public string TipoAlerta { get; set; }
    public string? Descripcion { get; set; }
    public string NivelRiesgo { get; set; }
    public DateTime FechaAlerta { get; set; }
    public string Estado { get; set; }
}

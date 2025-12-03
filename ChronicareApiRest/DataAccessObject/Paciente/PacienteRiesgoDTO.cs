namespace ChronicareApiRest.DataAccessObject.Paciente;

public class PacienteRiesgoDto
{
    public Guid IdPaciente { get; set; }
    public string Nombre { get; set; }
    public string Estado { get; set; }
    public string NivelRiesgo { get; set; }
    public string? TipoAlerta { get; set; }
    public string? AlertaDescripcion { get; set; }
    public DateTime? FechaAlerta { get; set; }
    public string? TipoRegistro { get; set; }
    public string? UltimoValor { get; set; }
}

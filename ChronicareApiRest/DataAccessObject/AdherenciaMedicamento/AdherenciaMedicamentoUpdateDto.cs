namespace ChronicareApiRest.DataAccessObject.AdherenciaMedicamento;

public class AdherenciaMedicamentoUpdateDto
{
    public Guid IdAdherencia { get; set; }
    public bool Tomado { get; set; }
    public string? Observacion { get; set; }
}

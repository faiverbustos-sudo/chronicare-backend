namespace ChronicareApiRest.DataAccessObject.AdherenciaMedicamento;

public class AdherenciaMedicamentoCreateDto
{
    public Guid IdMedicamento { get; set; }
    public Guid IdPaciente { get; set; }
    public DateTime FechaRegistro { get; set; }
    public bool Tomado { get; set; }
    public string? Observacion { get; set; }
}

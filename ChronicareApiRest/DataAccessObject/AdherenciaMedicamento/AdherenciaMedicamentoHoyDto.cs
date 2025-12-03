namespace ChronicareApiRest.DataAccessObject.AdherenciaMedicamento;

public class AdherenciaMedicamentoHoyDto
{
    public Guid IdMedicamento { get; set; }
    public Guid IdAdherencia { get; set; }
    public Guid IdPaciente { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public string Nombre { get; set; }
    public bool Tomado { get; set; }
}

using ChronicareApiRest.DataAccessObject.AdherenciaMedicamento;
using ChronicareApiRest.DataAccessObject.Medicamento;

namespace ChronicareApiRest.DataAccessObject.Paciente;

public class PacienteHoyDto
{
    public Guid IdPaciente { get; set; }
    public string Nombre { get; set; }
    public string? Presion { get; set; }
    public decimal? ValorNumerico { get; set; }
    public ICollection<AdherenciaMedicamentoHoyDto> AdherenciasMedicamentos { get; set; } = new List<AdherenciaMedicamentoHoyDto>();
}

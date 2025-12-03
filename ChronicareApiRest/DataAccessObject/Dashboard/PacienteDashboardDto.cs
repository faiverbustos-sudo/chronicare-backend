using ChronicareApiRest.DataAccessObject.Medicamento;

namespace ChronicareApiRest.DataAccessObject.Dashboard;

public class PacienteDashboardDto
{
    public Guid IdPaciente { get; set; }
    public string Nombre { get; set; }
    public string? AlertaDescripcion { get; set; }
    public DateTime? ProximaCita { get; set; }
    public string? ProximoControl { get; set; }
    public ICollection<MedicamentoReadDto> Medicamentos { get; set; } = new List<MedicamentoReadDto>();
    public ICollection<string?> Tareas { get; set; } = new List<string?>();
}

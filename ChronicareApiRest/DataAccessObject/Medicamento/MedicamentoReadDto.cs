namespace ChronicareApiRest.DataAccessObject.Medicamento;

public class MedicamentoReadDto
{
    public Guid IdMedicamento { get; set; }
    public Guid IdPaciente { get; set; }

    public string Nombre { get; set; }

    public string? Dosis { get; set; }

    public string? Frecuencia { get; set; }

    public string? Via { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
}

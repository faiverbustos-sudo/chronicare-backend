namespace ChronicareApiRest.DataAccessObject.Registro;

/// <summary>
/// DTO para la visualización de registros.
/// </summary>
public class RegistroReadDto
{
    /// <summary>
    /// Id del registro.
    /// </summary>
    public Guid IdRegistro { get; set; }

    /// <summary>
    /// Id del paciente asociado.
    /// </summary>
    public Guid IdPaciente { get; set; }

    /// <summary>
    /// Id del médico responsable.
    /// </summary>
    public Guid? IdMedico { get; set; }

    /// <summary>
    /// Fecha del registro.
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// Tipo del registro.
    /// </summary>
    public string TipoRegistro { get; set; }

    /// <summary>
    /// Valor numérico general.
    /// </summary>
    public decimal? ValorNumerico { get; set; }

    /// <summary>
    /// Valor sistólico.
    /// </summary>
    public int? ValorSistolica { get; set; }

    /// <summary>
    /// Valor diastólico.
    /// </summary>
    public int? ValorDiastolica { get; set; }

    /// <summary>
    /// Unidad del dato.
    /// </summary>
    public string? Unidad { get; set; }

    /// <summary>
    /// Observaciones adicionales.
    /// </summary>
    public string? Observaciones { get; set; }
}

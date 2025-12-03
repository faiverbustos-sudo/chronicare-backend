namespace ChronicareApiRest.DataAccessObject.Registro;

/// <summary>
/// DTO para la creación de un registro.
/// </summary>
public class RegistroCreateDto
{
    /// <summary>
    /// Id del paciente asociado.
    /// </summary>
    public Guid IdPaciente { get; set; }

    /// <summary>
    /// Id del médico responsable (opcional).
    /// </summary>
    public Guid? IdMedico { get; set; }

    /// <summary>
    /// Tipo del registro (glucosa, presión, oxígeno, etc.).
    /// </summary>
    public string TipoRegistro { get; set; }

    /// <summary>
    /// Valor numérico general cuando el registro lo requiera.
    /// </summary>
    public decimal? ValorNumerico { get; set; }

    /// <summary>
    /// Valor sistólico para presión arterial.
    /// </summary>
    public int? ValorSistolica { get; set; }

    /// <summary>
    /// Valor diastólico para presión arterial.
    /// </summary>
    public int? ValorDiastolica { get; set; }

    /// <summary>
    /// Unidad de medida del registro.
    /// </summary>
    public string? Unidad { get; set; }

    /// <summary>
    /// Observaciones adicionales.
    /// </summary>
    public string? Observaciones { get; set; }
}

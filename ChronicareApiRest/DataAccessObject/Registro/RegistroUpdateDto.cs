namespace ChronicareApiRest.DataAccessObject.Registro;

/// <summary>
/// DTO para la actualización de un registro.
/// </summary>
public class RegistroUpdateDto
{
    /// <summary>
    /// Tipo del registro.
    /// </summary>
    public string TipoRegistro { get; set; }

    /// <summary>
    /// Valor numérico general.
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
    /// Unidad del registro.
    /// </summary>
    public string? Unidad { get; set; }

    /// <summary>
    /// Observaciones adicionales.
    /// </summary>
    public string? Observaciones { get; set; }
}

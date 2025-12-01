namespace ChronicareApiRest.DataAccessObject.Paciente;

/// <summary>
/// DTO usado para actualizar información del paciente.
/// </summary>
public class PacienteUpdateDto
{
    /// <summary>
    /// Id del paciente.
    /// </summary>
    public Guid IdPaciente { get; set; }

    /// <summary>
    /// Documento del paciente.
    /// </summary>
    public required string Documento { get; set; }

    /// <summary>
    /// Tipo de documento, ej: CC, TI.
    /// </summary>
    public string? TipoDocumento { get; set; }

    /// <summary>
    /// Nombre completo del paciente.
    /// </summary>
    public required string Nombre { get; set; }

    /// <summary>
    /// Fecha de nacimiento.
    /// </summary>
    public DateTime? FechaNacimiento { get; set; }

    /// <summary>
    /// Género del paciente.
    /// </summary>
    public string? Genero { get; set; }

    /// <summary>
    /// Teléfono del paciente.
    /// </summary>
    public string? Telefono { get; set; }

    /// <summary>
    /// Email del paciente.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Dirección de residencia.
    /// </summary>
    public string? Direccion { get; set; }

    /// <summary>
    /// EPS del paciente.
    /// </summary>
    public string? Eps { get; set; }
}

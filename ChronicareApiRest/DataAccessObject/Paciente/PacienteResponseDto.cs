namespace ChronicareApiRest.DataAccessObject.Paciente;

/// <summary>
/// DTO usado para devolver información del paciente al cliente.
/// </summary>
public class PacienteResponseDto
{
    /// <summary>
    /// Identificador único del paciente.
    /// </summary>
    public Guid IdPaciente { get; set; }

    /// <summary>
    /// Documento de identidad.
    /// </summary>
    public string? Documento { get; set; }

    /// <summary>
    /// Tipo de documento (CC, TI, etc.).
    /// </summary>
    public string? TipoDocumento { get; set; }

    /// <summary>
    /// Nombre completo del paciente.
    /// </summary>
    public string Nombre { get; set; }

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
    /// Correo electrónico.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Dirección de residencia.
    /// </summary>
    public string? Direccion { get; set; }

    /// <summary>
    /// EPS afiliada.
    /// </summary>
    public string? Eps { get; set; }

    /// <summary>
    /// Fecha de registro del paciente.
    /// </summary>
    public DateTime FechaRegistro { get; set; }

    /// <summary>
    /// Estado actual del paciente (activo / inactivo).
    /// </summary>
    public string Estado { get; set; }
}

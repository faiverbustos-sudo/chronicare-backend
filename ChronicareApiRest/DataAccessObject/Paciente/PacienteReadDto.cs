namespace ChronicareApiRest.DataAccessObject.Paciente;

public class PacienteReadDto
{
    /// <summary>
    /// Identificador del paciente.
    /// </summary>
    public Guid IdPaciente { get; set; }

    /// <summary>
    /// Documento del paciente.
    /// </summary>
    public string? Documento { get; set; }

    /// <summary>
    /// Tipo de documento.
    /// </summary>
    public string? TipoDocumento { get; set; }

    /// <summary>
    /// Nombre del paciente.
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Estado: activo / inactivo.
    /// </summary>
    public string Estado { get; set; }

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

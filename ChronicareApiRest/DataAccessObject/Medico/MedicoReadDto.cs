namespace ChronicareApiRest.DataAccessObject.Medico;

public class MedicoReadDto
{
    /// <summary>
    /// Id del médico.
    /// </summary>
    public Guid IdMedico { get; set; }

    /// <summary>
    /// Nombre completo del médico.
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Especialidad del médico.
    /// </summary>
    public string? Especialidad { get; set; }

    /// <summary>
    /// Correo electrónico.
    /// </summary>
    public string? Email { get; set; }
}

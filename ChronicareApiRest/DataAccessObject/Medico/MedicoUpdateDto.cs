namespace ChronicareApiRest.DataAccessObject.Medico;

public class MedicoUpdateDto
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
    /// Registro médico profesional.
    /// </summary>
    public string? RegistroMedico { get; set; }

    /// <summary>
    /// Especialidad del médico.
    /// </summary>
    public string? Especialidad { get; set; }

    /// <summary>
    /// Correo electrónico.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Número telefónico.
    /// </summary>
    public string? Telefono { get; set; }

    /// <summary>
    /// Usuario del sistema asociado.
    /// </summary>
    public string? UsuarioSistema { get; set; }
}

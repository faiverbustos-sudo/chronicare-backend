using AutoMapper;
using ChronicareApiRest.DataAccessObject.Controller;
using ChronicareApiRest.DataAccessObject.Paciente;
using ChronicareApiRest.Entity;
using ChronicareApiRest.Identity;
using ChronicareApiRest.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

public class PacienteController : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly PacienteService _pacienteService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public PacienteController(
        ApplicationDbContext context, 
        PacienteService pacienteService,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _pacienteService = pacienteService;
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <summary>
    /// Lista todos los pacientes.
    /// </summary>
    /// <returns>Lista de pacientes.</returns>
    [Authorize(Roles = "Admin,Medico")]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetAll()
    {
        var pacientes = await _context.Pacientes.ToListAsync();
        var dto = _mapper.Map<List<PacienteReadDto>>(pacientes);

        return Ok(new APIResponse(true, "Listado obtenido", dto));
    }

    /// <summary>
    /// Obtiene un paciente por id.
    /// </summary>
    /// <param name="id">Id del paciente</param>
    /// <returns>Paciente encontrado</returns>
    [Authorize(Roles = "Admin,Medico")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<APIResponse>> GetById(Guid id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);

        if (paciente == null)
            return NotFound(new APIResponse(false, "Paciente no encontrado"));

        return Ok(new APIResponse(true, "Paciente obtenido", _mapper.Map<PacienteReadDto>(paciente)));
    }

    /// <summary>
    /// Crea un nuevo paciente.
    /// </summary>
    /// <param name="dto">Datos del paciente</param>
    [Authorize(Roles = "Admin,Medico")]
    [HttpPost]
    public async Task<ActionResult<APIResponse>> Create(PacienteCreateDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            Nombre = dto.Nombre
        };

        var splitName = dto.Nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var primerNombre = splitName[0].ToString();
        string primerasTres = primerNombre.Length >= 3 ? char.ToUpper(primerNombre[0]) + primerNombre.Substring(1, 2).ToLower() : char.ToUpper(primerNombre[0]) + primerNombre.Substring(1).ToLower();

        var password = primerasTres + dto.Documento;

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Paciente");
        }

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var paciente = _mapper.Map<Paciente>(dto);
        paciente.IdPaciente = Guid.NewGuid();

        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Paciente creado exitosamente", _mapper.Map<PacienteReadDto>(paciente)));
    }

    /// <summary>
    /// Edita un paciente existente.
    /// </summary>
    /// <param name="dto">Datos actualizados</param>
    [Authorize(Roles = "Admin,Medico")]
    [HttpPut]
    public async Task<ActionResult<APIResponse>> Update(PacienteUpdateDto dto)
    {
        var paciente = await _context.Pacientes.FindAsync(dto.IdPaciente);

        if (paciente == null)
            return NotFound(new APIResponse(false, "Paciente no encontrado"));

        _mapper.Map(dto, paciente);

        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Paciente actualizado", _mapper.Map<PacienteReadDto>(paciente)));
    }

    /// <summary>
    /// Cambia el estado del paciente a activo.
    /// </summary>
    /// <param name="id">Id del paciente</param>
    [Authorize(Roles = "Admin,Medico")]
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Activar(Guid id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);

        if (paciente == null)
            return NotFound(new APIResponse(false, "Paciente no encontrado"));

        paciente.Estado = "activo";
        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Paciente activado"));
    }

    /// <summary>
    /// Cambia el estado del paciente a inactivo.
    /// </summary>
    /// <param name="id">Id del paciente</param>
    [Authorize(Roles = "Admin,Medico")]
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Inactivar(Guid id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);

        if (paciente == null)
            return NotFound(new APIResponse(false, "Paciente no encontrado"));

        paciente.Estado = "inactivo";
        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Paciente inactivado"));
    }

    /// <summary>
    /// Regresa un dto con los datos de riesgo de un paciente.
    /// </summary>
    /// <returns>Lista de riesgos paciente.</returns>
    [Authorize(Roles = "Admin,Medico")]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> PacientesRiesgo()
    {
        var data = await _pacienteService.ObtenerPanelRiesgo();
        return Ok(data);
    }
}

using AutoMapper;
using ChronicareApiRest.DataAccessObject.Controller;
using ChronicareApiRest.DataAccessObject.Medico;
using ChronicareApiRest.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

[Authorize(Roles = "Admin,Medico")]
public class MedicoController : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MedicoController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Lista todos los médicos registrados.
    /// </summary>
    /// <returns>Listado de médicos.</returns>
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetAll()
    {
        var medicos = await _context.Medicos.ToListAsync();
        var dto = _mapper.Map<List<MedicoReadDto>>(medicos);

        return Ok(new APIResponse(true, "Listado obtenido correctamente", dto));
    }

    /// <summary>
    /// Obtiene un médico por su Id.
    /// </summary>
    /// <param name="id">Id del médico.</param>
    /// <returns>Médico encontrado.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<APIResponse>> GetById(Guid id)
    {
        var medico = await _context.Medicos.FindAsync(id);

        if (medico == null)
            return NotFound(new APIResponse(false, "Médico no encontrado"));

        return Ok(new APIResponse(true, "Médico obtenido", _mapper.Map<MedicoReadDto>(medico)));
    }

    /// <summary>
    /// Crea un nuevo médico.
    /// </summary>
    /// <param name="dto">Datos del médico.</param>
    [HttpPost]
    public async Task<ActionResult<APIResponse>> Create(MedicoCreateDto dto)
    {
        var medico = _mapper.Map<Medico>(dto);

        medico.IdMedico = Guid.NewGuid();
        medico.FechaRegistro = DateTime.UtcNow;

        _context.Medicos.Add(medico);
        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Médico creado correctamente", _mapper.Map<MedicoReadDto>(medico)));
    }

    /// <summary>
    /// Edita un médico existente.
    /// </summary>
    /// <param name="dto">Datos actualizados.</param>
    [HttpPut]
    public async Task<ActionResult<APIResponse>> Update(MedicoUpdateDto dto)
    {
        var medico = await _context.Medicos.FindAsync(dto.IdMedico);

        if (medico == null)
            return NotFound(new APIResponse(false, "Médico no encontrado"));

        _mapper.Map(dto, medico);

        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Médico actualizado", _mapper.Map<MedicoReadDto>(medico)));
    }

    /// <summary>
    /// Cambia el estado del médico a activo.
    /// </summary>
    /// <param name="id">Id del médico</param>
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Activar(Guid id)
    {
        var medico = await _context.Medicos.FindAsync(id);

        if (medico == null)
            return NotFound(new APIResponse(false, "Médico no encontrado"));

        medico.Estado = "activo";
        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Médico activado"));
    }

    /// <summary>
    /// Cambia el estado del médico a inactivo.
    /// </summary>
    /// <param name="id">Id del médico</param>
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Inactivar(Guid id)
    {
        var medico = await _context.Medicos.FindAsync(id);

        if (medico == null)
            return NotFound(new APIResponse(false, "Médico no encontrado"));

        medico.Estado = "inactivo";
        await _context.SaveChangesAsync();

        return Ok(new APIResponse(true, "Médico inactivado"));
    }
}

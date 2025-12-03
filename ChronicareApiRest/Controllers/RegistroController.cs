using AutoMapper;
using ChronicareApiRest.DataAccessObject.Controller;
using ChronicareApiRest.DataAccessObject.Registro;
using ChronicareApiRest.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

[Authorize(Roles = "Admin,Paciente")]
public class RegistroController : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public RegistroController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Lista todos los registros.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetAll()
    {
        var response = new APIResponse();
        try
        {
            var registros = await _context.Registros.AsNoTracking().ToListAsync();
            response.Result = _mapper.Map<List<RegistroReadDto>>(registros);
            response.IsSuccess = true;
            response.Message = "Datos obtenidos correctamente";
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error obteniendo datos";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Obtiene un registro por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<APIResponse>> GetById(Guid id)
    {
        var response = new APIResponse();
        try
        {
            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
            {
                response.IsSuccess = false;
                response.Message = "Registro no encontrado";
                return NotFound(response);
            }

            response.Result = _mapper.Map<RegistroReadDto>(registro);
            response.IsSuccess = true;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error obteniendo registro";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Crea un nuevo registro.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<APIResponse>> Create([FromBody] RegistroCreateDto dto)
    {
        var response = new APIResponse();
        try
        {
            var entity = _mapper.Map<Registro>(dto);
            entity.IdRegistro = Guid.NewGuid();
            entity.FechaRegistro = DateTime.UtcNow;

            _context.Registros.Add(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Registro creado";
            response.Result = _mapper.Map<RegistroReadDto>(entity);

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error creando registro";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Actualiza un registro existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Update(Guid id, [FromBody] RegistroUpdateDto dto)
    {
        var response = new APIResponse();
        try
        {
            var entity = await _context.Registros.FindAsync(id);
            if (entity == null)
            {
                response.IsSuccess = false;
                response.Message = "Registro no encontrado";
                return NotFound(response);
            }

            _mapper.Map(dto, entity);

            _context.Registros.Update(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Registro actualizado";
            response.Result = _mapper.Map<RegistroReadDto>(entity);

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error actualizando registro";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Elimina un registro.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Delete(Guid id)
    {
        var response = new APIResponse();
        try
        {
            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
            {
                response.IsSuccess = false;
                response.Message = "Registro no encontrado";
                return NotFound(response);
            }

            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Registro eliminado";

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error eliminando registro";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }
}

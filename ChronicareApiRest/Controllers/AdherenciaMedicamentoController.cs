using AutoMapper;
using ChronicareApiRest.DataAccessObject.AdherenciaMedicamento;
using ChronicareApiRest.DataAccessObject.Controller;
using ChronicareApiRest.Entity;
using ChronicareApiRest.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

public class AdherenciaMedicamentoController : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdherenciaMedicamentoController(
        ApplicationDbContext context, 
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <summary>
    /// Lista todos las adherencias de medicamentos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetAll()
    {
        var response = new APIResponse();
        try
        {
            var registros = await _context.AdherenciasMedicamento.AsNoTracking().ToListAsync();
            response.Result = _mapper.Map<List<AdherenciaMedicamentoReadDto>>(registros);
            response.IsSuccess = true;
            response.Message = "Datos obtenidos correctamente.";
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error obteniendo datos.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Obtiene una adherencia de medicamento por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<APIResponse>> GetById(Guid id)
    {
        var response = new APIResponse();
        try
        {
            var registro = await _context.AdherenciasMedicamento.FindAsync(id);
            if (registro == null)
            {
                response.IsSuccess = false;
                response.Message = "Adherencia de medicamento no encontrada.";
                return NotFound(response);
            }

            response.Result = _mapper.Map<AdherenciaMedicamentoReadDto>(registro);
            response.IsSuccess = true;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error obteniendo adherencia de medicamento.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Crea una nueva adherencia de medicamento.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<APIResponse>> Create([FromBody] AdherenciaMedicamentoCreateDto dto)
    {
        var response = new APIResponse();
        try
        {
            var entity = _mapper.Map<AdherenciaMedicamento>(dto);
            entity.IdAdherencia = Guid.NewGuid();
            entity.FechaRegistro = DateTime.UtcNow;

            _context.AdherenciasMedicamento.Add(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Adherencia de medicamento creada.";
            response.Result = _mapper.Map<AdherenciaMedicamentoReadDto>(entity);

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error creando adherencia de medicamento.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Actualiza una adherencia de medicamento existente.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<APIResponse>> Update([FromBody] AdherenciaMedicamentoUpdateDto dto)
    {
        var response = new APIResponse();
        try
        {
            var entity = await _context.AdherenciasMedicamento.FindAsync(dto.IdAdherencia);
            if (entity == null)
            {
                response.IsSuccess = false;
                response.Message = "Registro no encontrado.";
                return NotFound(response);
            }

            _mapper.Map(dto, entity);

            _context.AdherenciasMedicamento.Update(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Adherencia de medicamento actualizada.";
            response.Result = _mapper.Map<AdherenciaMedicamentoReadDto>(entity);

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
}

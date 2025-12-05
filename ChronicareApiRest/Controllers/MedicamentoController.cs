using AutoMapper;
using ChronicareApiRest.DataAccessObject.Controller;
using ChronicareApiRest.DataAccessObject.Medicamento;
using ChronicareApiRest.DataAccessObject.Paciente;
using ChronicareApiRest.DataAccessObject.Registro;
using ChronicareApiRest.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

[Authorize(Roles = "Admin,Medico")]
public class MedicamentoController : ApiControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MedicamentoController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Lista todos los medicamentos.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetAll()
    {
        var response = new APIResponse();
        try
        {
            var medicamentos = await _context.Medicamentos.AsNoTracking().ToListAsync();
            response.Result = _mapper.Map<List<MedicamentoReadDto>>(medicamentos);
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
    /// Lista todos los medicamentos por paciente.
    /// </summary>
    [HttpGet("{idPaciente:guid}")]
    public async Task<ActionResult<APIResponse>> GetAllByPaciente(Guid idPaciente)
    {
        var response = new APIResponse();
        try
        {
            var paciente = await _context.Pacientes.FindAsync(idPaciente);
            if(paciente == null)
            {
                response.IsSuccess = false;
                response.Message = "Paciente no encontrado.";
                return NotFound(response);
            }

            var medicamentos = await _context.Medicamentos
                .AsNoTracking()
                .Include(x => x.Paciente)
                .Where(x => x.IdPaciente == idPaciente)
                .ToListAsync();

            response.Result = new { Paciente = _mapper.Map<Paciente, PacienteReadDto>(paciente), Medicamentos = _mapper.Map<List<MedicamentoReadDto>>(medicamentos) };

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
    /// Obtiene un medicamento por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<APIResponse>> GetById(Guid id)
    {
        var response = new APIResponse();
        try
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                response.IsSuccess = false;
                response.Message = "Medicamento no encontrado.";
                return NotFound(response);
            }

            response.Result = _mapper.Map<RegistroReadDto>(medicamento);
            response.IsSuccess = true;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error obteniendo medicamento.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Crea un nuevo medicamento.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<APIResponse>> Create([FromBody] MedicamentoCreateDto dto)
    {
        var response = new APIResponse();
        try
        {
            var paciente = await _context.Pacientes.FindAsync(dto.IdPaciente);
            if (paciente == null)
            {
                response.IsSuccess = false;
                response.Message = "Paciente no encontrado.";
                return NotFound(response);
            }

            var entity = _mapper.Map<Medicamento>(dto);
            entity.IdMedicamento = Guid.NewGuid();
            entity.FechaCreacion = DateTime.UtcNow;
            entity.Activo = true;

            _context.Medicamentos.Add(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Medicamento creado.";
            response.Result = _mapper.Map<MedicamentoReadDto>(entity);

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error creando medicamento.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Actualiza un medicamento existente.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<APIResponse>> Update([FromBody] MedicamentoUpdateDto dto)
    {
        var response = new APIResponse();
        try
        {
            var entity = await _context.Medicamentos.FindAsync(dto.IdMedicamento);
            if (entity == null)
            {
                response.IsSuccess = false;
                response.Message = "Medicamento no encontrado.";
                return NotFound(response);
            }

            _mapper.Map(dto, entity);

            _context.Medicamentos.Update(entity);
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Medicamento actualizado.";
            response.Result = _mapper.Map<MedicamentoReadDto>(entity);

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error actualizando medicamento.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Cambia el estado del paciente a activo.
    /// </summary>
    /// <param name="id">Id del paciente</param>
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Activar(Guid id)
    {
        var response = new APIResponse();
        try
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null)
                return NotFound(new APIResponse(false, "Medicamento no encontrado"));

            medicamento.Activo = true;
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Medicamento activado.";

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error actualizando medicamento.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }        
    }

    /// <summary>
    /// Cambia el estado del medicamento a inactivo.
    /// </summary>
    /// <param name="id">Id del medicamento</param>    
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<APIResponse>> Inactivar(Guid id)
    {
        var response = new APIResponse();

        try
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null)
                return NotFound(new APIResponse(false, "Medicamento no encontrado"));

            medicamento.Activo = false;
            await _context.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = "Medicamento inactivado.";

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error actualizando medicamento.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }        
    }
}

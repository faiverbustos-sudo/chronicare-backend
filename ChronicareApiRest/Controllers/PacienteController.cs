using System.Security.Claims;
using AutoMapper;
using ChronicareApiRest.DataAccessObject.AdherenciaMedicamento;
using ChronicareApiRest.DataAccessObject.Controller;
using ChronicareApiRest.DataAccessObject.Dashboard;
using ChronicareApiRest.DataAccessObject.Medicamento;
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin,Paciente")]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> PacienteDashboard()
    {
        var response = new APIResponse();
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized("No se pudo obtener el usuario autenticado.");

            var paciente = await _context.Pacientes
                .Include(p => p.Medicamentos)
                .Include(p => p.Tareas)
                .Include(p => p.Alertas)
                .FirstOrDefaultAsync(p => p.IdUsuario.ToString() == userId);

            if (paciente == null)
                return NotFound("No existe un paciente asociado a este usuario.");

            var ultimaAlerta = paciente.Alertas.OrderByDescending(a => a.FechaAlerta).Select(a => a.Descripcion).FirstOrDefault();

            var dashboard = new PacienteDashboardDto()
            {
                IdPaciente = paciente.IdPaciente,
                Nombre = paciente.Nombre,
                AlertaDescripcion = ultimaAlerta != null ? ultimaAlerta : "Sin alertas.",
                ProximaCita = paciente.ProximaCita,
                ProximoControl = paciente.ProximoControl,
                Tareas = paciente.Tareas.Where(a => a.Completada == false).Select(a => a.Descripcion).ToList(),
                Medicamentos = _mapper.Map<ICollection<MedicamentoReadDto>>(paciente.Medicamentos)
            };

            response.IsSuccess = true;
            response.Message = "Paciente encontrado.";
            response.Result = dashboard;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error obteniendo dashboard de paciente.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }


    [Authorize(Roles = "Admin,Paciente")]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> PacienteHoy()
    {
        var response = new APIResponse();
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized("No se pudo obtener el usuario autenticado.");

            var paciente = await _context.Pacientes
                .Include(p => p.Medicamentos)
                .Include(p => p.Tareas)
                .Include(p => p.Alertas)
                .Include(p => p.AdherenciasMedicamento)
                .Include(p => p.Registros)
                .FirstOrDefaultAsync(p => p.IdUsuario.ToString() == userId);

            if (paciente == null)
                return NotFound("No existe un paciente asociado a este usuario.");

            var NowDate = DateTime.Now.Date;
            var medicamentosHoy = paciente.Medicamentos.Where(x => NowDate >= x.FechaInicio && NowDate <= x.FechaFin && x.Activo);

            var adherencias = new List<AdherenciaMedicamentoHoyDto>();
            foreach(var medicamento in medicamentosHoy)
            {
                var adherenciaMedicamentos = paciente.AdherenciasMedicamento.Where(x => x.IdMedicamento == medicamento.IdMedicamento && x.IdPaciente == paciente.IdPaciente && x.FechaRegistro.Date == NowDate);
                if(adherenciaMedicamentos.Any())
                {
                    foreach (var adherencia in adherenciaMedicamentos)
                    {
                        var adherenciaHoy = new AdherenciaMedicamentoHoyDto
                        {
                            IdAdherencia = adherencia.IdAdherencia,
                            IdPaciente = adherencia.IdPaciente,
                            IdMedicamento = adherencia.IdMedicamento,
                            Nombre = medicamento.Nombre,
                            FechaRegistro = adherencia.FechaRegistro,
                            Tomado = adherencia.Tomado
                        };
                        adherencias.Add(adherenciaHoy);
                    }
                }
                else
                {
                    var adherenciaHoy = new AdherenciaMedicamentoHoyDto
                    {
                        IdAdherencia = Guid.Empty,
                        IdPaciente = paciente.IdPaciente,
                        IdMedicamento = medicamento.IdMedicamento,
                        Nombre = medicamento.Nombre,
                        FechaRegistro = null,
                        Tomado = false
                    };
                    adherencias.Add(adherenciaHoy);
                }
                
            }

            var registroPresion = paciente.Registros
                .OrderByDescending(r => r.FechaRegistro)
                .FirstOrDefault(x => x.FechaRegistro.Date == NowDate && x.TipoRegistro == "presion" && x.IdPaciente == paciente.IdPaciente);
            var presion = registroPresion != null ? $"{registroPresion.ValorSistolica}/{registroPresion.ValorDiastolica}" : "140/90";

            var registroGlucosa = paciente.Registros
                .OrderByDescending(r => r.FechaRegistro)
                .FirstOrDefault(x => x.FechaRegistro.Date == NowDate && x.TipoRegistro == "glucosa" && x.IdPaciente == paciente.IdPaciente);
            var glucosa = registroGlucosa != null ? registroGlucosa.ValorNumerico : 0;

            var data = new PacienteHoyDto()
            {
                IdPaciente = paciente.IdPaciente,
                Nombre = paciente.Nombre,
                Presion = presion,
                ValorNumerico = glucosa,
                AdherenciasMedicamentos = adherencias
            };

            response.IsSuccess = true;
            response.Message = "Paciente encontrado.";
            response.Result = data;

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Error obteniendo datos de hoy del paciente.";
            response.Errors = new List<string> { ex.Message };
            return BadRequest(response);
        }
    }
}

using ChronicareApiRest.DataAccessObject.Controller;
using ChronicareApiRest.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

public class PacienteController : ApiControllerBase
{
    private readonly AppDbContext _context;
    private readonly PacienteService _pacienteService;

    public PacienteController(AppDbContext context, PacienteService pacienteService)
    {
        _context = context;
        _pacienteService = pacienteService;
    }

    [HttpGet]
    public async Task<ActionResult<Response>> All()
    {
        var productos = await _context.Pacientes.ToListAsync();
        return Ok(productos);
    }

    [HttpGet]
    public async Task<ActionResult<Response>> PacientesRiesgo()
    {
        var data = await _pacienteService.ObtenerPanelRiesgo();
        return Ok(data);
    }
}

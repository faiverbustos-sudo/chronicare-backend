using ChronicareApiRest.DataAccessObject.Paciente;
using Microsoft.EntityFrameworkCore;

namespace ChronicareApiRest.Service;

public class PacienteService
{
    private readonly ApplicationDbContext _context;

    public PacienteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PacienteRiesgoDto>> ObtenerPanelRiesgo()
    {
        return await _context.Pacientes
        .Select(p => new PacienteRiesgoDto
        {
            IdPaciente = p.IdPaciente,
            Nombre = p.Nombre,
            Estado = p.Estado,

            NivelRiesgo = p.Riesgos
                .OrderByDescending(r => r.FechaActualizacion)
                .Select(r => r.NivelRiesgo)
                .FirstOrDefault(),

            TipoAlerta = p.Alertas
                .OrderByDescending(a => a.FechaAlerta)
                .Select(a => a.TipoAlerta)
                .FirstOrDefault(),

            AlertaDescripcion = p.Alertas
                .OrderByDescending(a => a.FechaAlerta)
                .Select(a => a.Descripcion)
                .FirstOrDefault(),

            FechaAlerta = p.Alertas
                .OrderByDescending(a => a.FechaAlerta)
                .Select(a => a.FechaAlerta)
                .FirstOrDefault(),

            TipoRegistro = p.Registros
                .OrderByDescending(r => r.FechaRegistro)
                .Select(r => r.TipoRegistro)
                .FirstOrDefault(),

            UltimoValor = p.Registros
                .OrderByDescending(r => r.FechaRegistro)
                .Select(r =>
                    r.ValorNumerico != null ? r.ValorNumerico.ToString() :
                    (r.ValorSistolica != null ? $"{r.ValorSistolica}/{r.ValorDiastolica}" :
                    r.Observaciones)
                )
                .FirstOrDefault()
        })
        .ToListAsync();
    }
}

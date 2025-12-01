using ChronicareApiRest.DataAccessObject.Login;
using ChronicareApiRest.Service;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // 🔐 Validar usuario en DB
        if (request.Usuario != "admin" || request.Password != "1234")
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });

        // Generar token
        var token = _jwtService.GenerarToken("1", "Administrador");

        return Ok(new
        {
            token,
            usuario = request.Usuario
        });
    }
}

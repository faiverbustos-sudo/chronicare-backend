using ChronicareApiRest.DataAccessObject.Identity;
using ChronicareApiRest.DataAccessObject.Login;
using ChronicareApiRest.Identity;
using ChronicareApiRest.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace ChronicareApiRest.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly JwtIdentityService _jwtService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, 
        JwtIdentityService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    /*
    [HttpPost]
    public IActionResult Login2([FromBody] LoginRequest request)
    {
        
        // 🔐 Validar usuario en DB
        if (request.Usuario != "admin" || request.Password != "1234")
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });

        // Generar token
        //var token = _jwtService.GenerarToken("1", "Administrador");

        return Ok(new
        {
            //token,
            usuario = request.Usuario
        });
        return Ok();
    }
    */

    // Registrar usuario
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            Nombre = model.Nombre
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { mensaje = "Usuario registrado" });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
            return Unauthorized(new { mensaje = "Credenciales incorrectas" });

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!result.Succeeded)
            return Unauthorized(new { mensaje = "Credenciales incorrectas" });

        var token = await _jwtService.GenerateToken(user);

        return Ok(new { token });
    }
}

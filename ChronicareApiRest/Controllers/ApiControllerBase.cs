using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("/api/chronicare/[controller]/[action]")]
[ApiController]
public abstract class ApiControllerBase : Controller
{
}


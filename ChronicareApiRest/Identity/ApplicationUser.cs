using Microsoft.AspNetCore.Identity;

namespace ChronicareApiRest.Identity;

public class ApplicationUser : IdentityUser
{
    public string Nombre { get; set; }
}

using Microsoft.AspNetCore.Identity;

namespace Management.Services.AuthAPI.Model
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}

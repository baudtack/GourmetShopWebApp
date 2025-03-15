using GourmetShopWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace GourmetShopWebApp
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Customer Customer { get; set; } = null!;
    }
}

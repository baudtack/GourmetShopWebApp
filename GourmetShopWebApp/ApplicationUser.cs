using GourmetShopWebApp.Data;
using GourmetShopWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GourmetShopWebApp
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Customer Customer { get; set; } = null!;

        public int GetCartCount(ApplicationDbContext Context)
        {
            var cust = Context.Customers.FirstOrDefault(c => c.User == this);
            var carts = Context.Carts.Where(c => c.Customer == cust);
            return carts.Sum(c => c.Quantity);   
        }
    }
}

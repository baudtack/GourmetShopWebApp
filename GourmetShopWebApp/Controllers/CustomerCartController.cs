using GourmetShopWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GourmetShopWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
namespace GourmetShopWebApp.Controllers
{
    
    public class CustomerCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public CustomerCartController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context;
            _usermanager = um;
        }

        [Authorize]
        public async Task<IActionResult> ViewCart()
        {

            var user = await _usermanager.GetUserAsync(User);
            
            if (user.Customer == null)
            {
                user.Customer = new Customer();                
            }

            var cartitems = from cart in _context.Carts where cart.Customer == user.Customer select cart;

            return View(cartitems);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int ProductId)
        {
            var user = await _usermanager.GetUserAsync(User);

            var prod = _context.Products.FirstOrDefault(p => p.Id == ProductId);

            var cartitem = _context.Carts.FirstOrDefault(c => c.Customer == user.Customer && c.Product == prod);

            if (cartitem == null) {
                var c = new Cart();
                c.ProductId = ProductId;
                c.Product = prod;
                c.Customer = user.Customer;
                c.Quantity = 1;
                c.UnitPrice = (decimal)prod.UnitPrice;
                _context.Add(c);
            } else {
                cartitem.Quantity = cartitem.Quantity + 1;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCart");
        }
    }
}

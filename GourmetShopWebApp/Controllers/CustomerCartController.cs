using GourmetShopWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GourmetShopWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Configuration;
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
        public async Task<IActionResult> Checkout()
        {
            var user = await _usermanager.GetUserAsync(User);
            var cust = _context.Customers.FirstOrDefault(c => c.User == user);
            var carts = _context.Carts.Where(c => c.Customer == cust).ToList();
            var last = Int32.Parse(_context.Orders.Max(o => o.OrderNumber));
            

            var order = new Order();
            order.Customer = cust;
            order.OrderDate = DateTime.Now;
            order.OrderNumber = (last + 1).ToString();
            decimal total = 0;

            foreach(var cart in carts)
            {
                var oi = new OrderItem();
                oi.ProductId = cart.ProductId;
                oi.UnitPrice = cart.UnitPrice;
                oi.Quantity = cart.Quantity;
                oi.Order = order;
                total += oi.Quantity * oi.UnitPrice;
                _context.OrderItems.Add(oi);
                _context.Carts.Remove(cart);
            }

            order.TotalAmount = total;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return View();
        }


        [Authorize]
        public async Task<IActionResult> ViewCart()
        {
            var user = await _usermanager.GetUserAsync(User);

            if (user.Customer == null)
            {
                var cust = _context.Customers.FirstOrDefault(c => c.User == user);

                if (cust == null)
                {
                    user.Customer = new Customer();
                    user.Customer.User = user;
                }
                else
                {
                    user.Customer = cust;
                }

                await _context.SaveChangesAsync();
            }

            var cartitems = from cart in _context.Carts where cart.Customer == user.Customer select cart;
            var ca = await cartitems.Include(c => c.Product).ToArrayAsync();

            return View(ca);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int ProductId)
        {
            var user = await _usermanager.GetUserAsync(User);

            if (user.Customer == null)
            {
                var cust = _context.Customers.FirstOrDefault(c => c.User == user);

                if (cust == null)
                {
                    user.Customer = new Customer();
                    user.Customer.User = user;
                }
                else
                {
                    user.Customer = cust;
                }
            }

            var prod = _context.Products.FirstOrDefault(p => p.Id == ProductId);

            var cartitem = _context.Carts.FirstOrDefault(c => c.Customer == user.Customer && c.Product == prod);

            if (cartitem == null)
            {
                var c = new Cart();
                c.ProductId = ProductId;
                c.Product = prod;
                c.Customer = user.Customer;

                c.Quantity = 1;
                c.UnitPrice = (decimal)prod.UnitPrice;
                _context.Add(c);
            }
            else
            {
                cartitem.Quantity = cartitem.Quantity + 1;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("ViewCart");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            var user = await _usermanager.GetUserAsync(User);
            if (cart.Customer == user.Customer)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewCart));
            } else
            {
                return NotFound();
            }
        }
    }
}

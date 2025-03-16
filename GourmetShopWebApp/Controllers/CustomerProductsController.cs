using GourmetShopWebApp.Data;
using GourmetShopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GourmetShopWebApp.Controllers
{
    public class CustomerProductsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CustomerProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<IActionResult> Index(string searchstring, string sortorder)
        {

            ViewData["CurrentFilter"] = searchstring;
            ViewData["ProductSortParam"] = sortorder == "name" ? "name_desc" : "name";
            ViewData["PriceSortParam"] = sortorder == "price" ? "price_desc" : "price";

            var products = from prod in _context.Products where prod.IsDiscontinued == false select prod;

            if (!String.IsNullOrEmpty(searchstring))
            {
                products = products.Where(p => p.ProductName.Contains(searchstring));
            }

            products = products.Include(p => p.Supplier);

            switch (sortorder)
            {
                case "name":
                    products = products.OrderBy(p => p.ProductName);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break;
                case "price":
                    products = products.OrderBy(p => p.UnitPrice);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.UnitPrice);
                    break;

            }
            
           

            return View(products);
        }
    }
}

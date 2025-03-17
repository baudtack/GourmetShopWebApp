using GourmetShopWebApp.Models;

namespace GourmetShopWebApp.ViewModels
{
    public class SupplierSearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

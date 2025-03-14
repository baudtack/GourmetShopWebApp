using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GourmetShopWebApp.Models;

namespace GourmetShopWebApp.Models;

public partial class Product
{
    public int Id { get; set; }

    [Display(Name = "Product")]
    public string ProductName { get; set; } = null!;

    public int SupplierId { get; set; }
    [Display(Name = "Price")]
    public decimal? UnitPrice { get; set; }

    public string? Package { get; set; }

    public bool IsDiscontinued { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Supplier Supplier { get; set; } = new Supplier();
}

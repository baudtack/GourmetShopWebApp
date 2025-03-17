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

 
    [Display(Name = "Price")]
    public decimal? UnitPrice { get; set; }

    public string? Package { get; set; }

    public bool IsDiscontinued { get; set; }
    public int? CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public int SupplierId { get; set; }
    public virtual Supplier? Supplier { get; set; }

    // Sale properties
    public bool IsOnSale { get; set; }
    public decimal? SalePrice { get; set; }
    public DateTime? SaleStart { get; set; }
    public DateTime? SaleEnd { get; set; }

    // Computed property that returns the sale price if the product is on sale during the valid period; otherwise, it returns the regular unit price.
    public decimal? CurrentPrice
    {
        get
        {
            if (IsOnSale && SalePrice.HasValue && SaleStart.HasValue && SaleEnd.HasValue)
            {
                DateTime now = DateTime.Now;
                if (now >= SaleStart.Value && now <= SaleEnd.Value)
                {
                    return SalePrice.Value;
                }
            }
            return UnitPrice;
        }
    }
}

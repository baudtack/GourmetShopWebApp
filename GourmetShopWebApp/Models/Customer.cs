using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GourmetShopWebApp.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? FirstName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? LoginName { get; set; }

    public byte[]? PasswordHash { get; set; }

    public Guid? Salt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

   [ForeignKey("UserId")]
   public virtual ApplicationUser? User { get; set; } 
}

using System;
using System.Collections.Generic;

namespace TrollMarket.DataAcces.Models;

public partial class Shipper
{
    public string ShipperNumber { get; set; } = null!;

    public string? ShipperName { get; set; }

    public decimal Price { get; set; }

    public bool Service { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

using System;
using System.Collections.Generic;

namespace TrollMarket.DataAcces.Models;

public partial class Seller
{
    public string SellerNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Balance { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

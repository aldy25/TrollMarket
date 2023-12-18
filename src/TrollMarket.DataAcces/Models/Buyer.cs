using System;
using System.Collections.Generic;

namespace TrollMarket.DataAcces.Models;

public partial class Buyer
{
    public string BuyerNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Balance { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<HistoryToUp> HistoryToUps { get; set; } = new List<HistoryToUp>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

using System;
using System.Collections.Generic;

namespace TrollMarket.DataAcces.Models;

public partial class Cart
{
    public int ProductId { get; set; }

    public string BuyerNumber { get; set; } = null!;

    public string ShipperNumber { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Buyer BuyerNumberNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Shipper ShipperNumberNavigation { get; set; } = null!;
}

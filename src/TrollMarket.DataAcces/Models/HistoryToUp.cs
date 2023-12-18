using System;
using System.Collections.Generic;

namespace TrollMarket.DataAcces.Models;

public partial class HistoryToUp
{
    public int Id { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public decimal Amount { get; set; }

    public string BuyerNumber { get; set; } = null!;
    public DateTime TopUpDate { get; set; }

    public virtual Buyer BuyerNumberNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace TrollMarket.DataAcces.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual Buyer? Buyer { get; set; }

    public virtual Seller? Seller { get; set; }
}

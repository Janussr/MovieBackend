using System;
using System.Collections.Generic;

namespace Movie.Api;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public int MovieId { get; set; }

    public int Quantity { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}

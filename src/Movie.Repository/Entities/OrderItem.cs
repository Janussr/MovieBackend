using System;
using System.Collections.Generic;

namespace Movie.Api;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int MovieId { get; set; }

    public int Quantity { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}

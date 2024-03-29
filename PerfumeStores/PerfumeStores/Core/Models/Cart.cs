﻿using System;
using System.Collections.Generic;

namespace PerfumeStores.Core.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public short Quantity { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Product Product { get; set; }
}

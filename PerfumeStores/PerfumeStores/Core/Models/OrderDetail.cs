using System;
using System.Collections.Generic;

namespace PerfumeStores.Core.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public string ShippingAddress { get; set; }

    public short Quantity { get; set; }

    public double TotalPrice { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}

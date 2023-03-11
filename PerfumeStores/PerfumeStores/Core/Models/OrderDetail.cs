using System;
using System.Collections.Generic;

namespace PerfumeStores.Core.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    public string OrderStatus { get; set; }

    public int ShippingAddress { get; set; }

    public short Quantity { get; set; }

    public double TotalPrice { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Product Product { get; set; }
}

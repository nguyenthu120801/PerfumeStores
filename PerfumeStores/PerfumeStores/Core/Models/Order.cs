using System;
using System.Collections.Generic;

namespace PerfumeStores.Core.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreateDate { get; set; }

    public int? CounpId { get; set; }

    public decimal Total { get; set; }

    public string OrderStatus { get; set; }

    public bool IsPaid { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}

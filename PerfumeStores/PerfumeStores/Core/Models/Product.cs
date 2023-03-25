using System;
using System.Collections.Generic;

namespace PerfumeStores.Core.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool Status { get; set; }

    public string Origin { get; set; }

    public int Capacity { get; set; }

    public string Image { get; set; }

    public double Price { get; set; }

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}

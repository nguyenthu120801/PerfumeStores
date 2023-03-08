using System;
using System.Collections.Generic;

namespace PerfumeStores.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderDetailId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string OrderStatus { get; set; } = null!;
        public int ShippingAddress { get; set; }
        public short Quantity { get; set; }
        public double TotalPrice { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}

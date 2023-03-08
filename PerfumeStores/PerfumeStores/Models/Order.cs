using System;
using System.Collections.Generic;

namespace PerfumeStores.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CounpId { get; set; }
        public bool IsPaid { get; set; }

        public virtual OrderDetail OrderDetail { get; set; } = null!;
    }
}

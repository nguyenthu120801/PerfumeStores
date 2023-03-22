using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.Models;

namespace PerfumeStores.Pages.OrderManage
{
    public class DetailsModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;

        public DetailsModel(PerfumeStores.Core.Models.Prn221Context context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!;
        public OrderDetail OrderDetail { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(x=>x.Customer)
                .Include(x=>x.OrderDetails)
                .ThenInclude(x=>x.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
                OrderDetail = order.OrderDetails.FirstOrDefault();
                Product = OrderDetail.Product;
            }
            return Page();
        }
    }
}

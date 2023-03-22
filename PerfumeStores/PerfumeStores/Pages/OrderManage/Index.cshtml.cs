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
    public class IndexModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;

        public IndexModel(PerfumeStores.Core.Models.Prn221Context context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
            }
        }
    }
}

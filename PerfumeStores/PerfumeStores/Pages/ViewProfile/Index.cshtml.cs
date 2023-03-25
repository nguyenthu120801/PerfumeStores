using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.Models;

namespace PerfumeStores.Pages.ViewProfile
{
    public class IndexModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;

        public IndexModel(PerfumeStores.Core.Models.Prn221Context context)
        {
            _context = context;
        }

        public Customer cus { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                cus = customer;
            }
                return Page();
        }
    }
}

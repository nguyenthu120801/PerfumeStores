using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.Models;
using PerfumeStores.Services;

namespace PerfumeStores.Pages.ViewProfile
{
    public class UpdateModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public UpdateModel(PerfumeStores.Core.Models.Prn221Context context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Customer cus { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            cus = customer;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            

            _context.Attach(cus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(cus.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Page();
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(c => c.CustomerId == id)).GetValueOrDefault();
        }
    }
}

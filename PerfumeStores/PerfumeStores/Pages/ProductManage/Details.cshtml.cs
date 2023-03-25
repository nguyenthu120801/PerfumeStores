using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.Models;

namespace PerfumeStores.Pages.ProductManage
{
    public class DetailsModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;

        public DetailsModel(PerfumeStores.Core.Models.Prn221Context context)
        {
            _context = context;
        }

        public Product Product { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;
using PerfumeStores.Services.Services;

namespace PerfumeStores.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly Prn221Context _context;

        public IndexModel(IProductService productService, Prn221Context context)
        {
            _productService = productService;
            _context = context;
        }

        [BindProperty]
        public int IndexPaging { get; set; }

        public async Task OnGet()
        {
            ViewData["Category"] = _context.Categories.ToList();
            ViewData["ProductPaging"] = _productService.ProductPaging(IndexPaging);
        }
    }
}
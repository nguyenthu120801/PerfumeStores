using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;

namespace PerfumeStores.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IShoppingCart _shoppingCart;
        private readonly Prn221Context _context;

        public IndexModel(IProductService productService, IShoppingCart shoppingCart, Prn221Context context)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
            _context = context;
        }

        [BindProperty]
        public int CateId { get; set; }

        [BindProperty]
        public int IndexPaging { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty]
        public int ProductId { get; set; }

        public async Task OnGet()
        {
            ViewData["Category"] = _context.Categories.ToList();
            ViewData["ProductPaging"] = _productService.ProductPaging(CateId, IndexPaging, SearchString);
        }

        public async void OnPostAddCart()
        {
            await _shoppingCart.AddToCart(ProductId);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;
using PerfumeStores.Services.Services;

namespace PerfumeStores.Pages.Shopping
{
    public class ProductModel : PageModel
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly Prn221Context _context;
        private readonly IProductService _productService;

        public ProductModel(IShoppingCart shoppingCart, Prn221Context context, IProductService productService)
        {
            _shoppingCart = shoppingCart;
            _context = context;
            _productService = productService;
        }

        [BindProperty(SupportsGet = true)]
        public int CateId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int IndexPaging { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGet()
        {
            ViewData["TotalPage"] = await  _productService.TotalPage(CateId, IndexPaging, SearchString);
            ViewData["Category"] = await _context.Categories.ToListAsync();
            ViewData["Product"] = await _productService.ProductPaging(CateId, IndexPaging, SearchString);
            ViewData["TotalFound"] = await _productService.TotalFound(CateId, IndexPaging, SearchString);
        }

        public async Task<IActionResult> OnGetAddToCart(int productId)
        {
            await _shoppingCart.AddToCart(productId, (int)LoginDTO.CustomerID);
            var cartCount = (await _shoppingCart.GetCartItems((int)LoginDTO.CustomerID)).Count;
            await OnGet();
            return new JsonResult(cartCount);
        }
    }
}

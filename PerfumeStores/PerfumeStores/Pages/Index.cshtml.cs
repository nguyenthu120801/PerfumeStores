using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;
using PerfumeStores.Pages.Shared;

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

        [BindProperty(SupportsGet = true)]
        public int CateId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGet()
        {
            ViewData["Category"] = await _context.Categories.ToListAsync();
            ViewData["ProductHome"] = await _productService.ProductHome();
            LayoutModel.AmountCart = LoginDTO.CustomerID != null ? (await _shoppingCart.GetCartItems((int)LoginDTO.CustomerID)).Count : 0;
        }

        public async Task<IActionResult> OnGetAddToCart(int productId)
        {
            if (LoginDTO.CustomerID == null)
            {
                return Redirect("/login");
            }
            else
            {
                await _shoppingCart.AddToCart(productId, (int)LoginDTO.CustomerID);
                var cartCount = (await _shoppingCart.GetCartItems((int)LoginDTO.CustomerID)).Count;
                await OnGet();
                return new JsonResult(cartCount);
            }
        }
    }
}
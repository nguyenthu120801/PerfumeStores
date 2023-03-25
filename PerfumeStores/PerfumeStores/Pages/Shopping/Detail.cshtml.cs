using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;
using PerfumeStores.Pages.Shared;

namespace PerfumeStores.Pages.Shopping
{
    public class DetailModel : PageModel
    {
        private readonly Prn221Context _context;
        private readonly IShoppingCart _shoppingCart;

        public DetailModel(Prn221Context context, IShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        [BindProperty(SupportsGet = true)]
        public int ProductID { get; set; }

        [BindProperty(Name ="id", SupportsGet = true)]
        public int Id { get; set; }

        public async Task OnGet()
        {
            ViewData["ProductDetail"] = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == ProductID);
        }

        public async Task<IActionResult> OnGetAddToCart()
        {
            if (LoginDTO.CustomerID == null)
            {
                return Redirect("/login");
            }
            else
            {
                await _shoppingCart.AddToCart(Id, (int)LoginDTO.CustomerID);
                LayoutModel.AmountCart = (await _shoppingCart.GetCartItems((int)LoginDTO.CustomerID)).Count;
                return Redirect($"/shopping/detail?productid={Id}");
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Services;
using PerfumeStores.Pages.Shared;

namespace PerfumeStores.Pages.Shopping
{
    public class CartModel : PageModel
    {
        private readonly IShoppingCart _shoppingCart;

        public CartModel(IShoppingCart shoppingCart) => _shoppingCart= shoppingCart;

        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (LoginDTO.CustomerID != null)
            {
                ViewData["Cart"] = await _shoppingCart.GetCartItems((int)LoginDTO.CustomerID);
                ViewData["Total"] = await _shoppingCart.GetTotal((int)LoginDTO.CustomerID);
                return Page();
            }
            else
                return Redirect("/login");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _shoppingCart.RemoveFromCart(Id, (int)LoginDTO.CustomerID);
            LayoutModel.AmountCart = (await _shoppingCart.GetCartItems((int)LoginDTO.CustomerID)).Count;
            return Redirect("/shopping/cart");
        }

    }
}

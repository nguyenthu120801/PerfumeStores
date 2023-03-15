using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Services;

namespace PerfumeStores.Pages.Shopping
{
    public class CartModel : PageModel
    {
        private readonly IShoppingCart _shoppingCart;

        public CartModel(IShoppingCart shoppingCart) => _shoppingCart= shoppingCart;

        [BindProperty]
        public int Id { get; set; }

        public async void OnGet()
        {
            ViewData["Cart"] = await _shoppingCart.GetCartItems();
            ViewData["Total"] = await _shoppingCart.GetTotal();
        }

        public async void OnPostDelete()
        {
            await _shoppingCart.RemoveFromCart(Id);
        }

    }
}

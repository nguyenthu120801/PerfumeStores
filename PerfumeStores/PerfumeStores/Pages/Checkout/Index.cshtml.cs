using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;

namespace PerfumeStores.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        private readonly Prn221Context _context;
        private readonly IShoppingCart _shoppingCart;

        public IndexModel(Prn221Context context, IShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        [BindProperty(SupportsGet = true)]
        public string Address { get; set; }
        [BindProperty(SupportsGet = true)]
        public string City { get; set; }

        public async Task OnGet()
        {
            ViewData["Customer"] = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == LoginDTO.CustomerID);
            ViewData["Cart"] = await _shoppingCart.GetCartItems((int)LoginDTO.CustomerID);
            ViewData["Total"] = await _shoppingCart.GetTotal((int)LoginDTO.CustomerID);
        }
        
        public async Task<IActionResult> OnPost()
        {
            var order = new Order
            {
                CustomerId = (int)LoginDTO.CustomerID,
                CreateDate = DateTime.Now,
                IsPaid = false,
                Total = await _shoppingCart.GetTotal((int)LoginDTO.CustomerID),
                OrderStatus = "Waiting for confirmation",
            };
            await _shoppingCart.CreateOrder(order, $"{Address}, {City}", (int)LoginDTO.CustomerID);
            return Redirect("/shopping/order");
        }
    }
}

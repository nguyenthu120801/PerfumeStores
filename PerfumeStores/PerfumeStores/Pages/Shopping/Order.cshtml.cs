using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;

namespace PerfumeStores.Pages.Shopping
{
    public class OrderModel : PageModel
    {
        private readonly Prn221Context _context;

        public OrderModel(Prn221Context context)
        {
            _context = context;
        }

        [BindProperty(Name = "id", SupportsGet = true)]
        public int OrderId { get; set; }

        public IActionResult OnGet()
        {
            if (LoginDTO.CustomerID == null)
                return Redirect("/login");
            else
            {
                ViewData["Order"] = _context.Orders.Where(x => x.CustomerId == LoginDTO.CustomerID).ToList();
                return Page();
            }
        }

        public IActionResult OnGetDelete()
        {
            _context.OrderDetails.RemoveRange(_context.OrderDetails.Where(x => x.OrderId == OrderId).ToList());
            _context.Orders.Remove(_context.Orders.FirstOrDefault(x => x.OrderId == OrderId));
            _context.SaveChanges();
            return RedirectToAction("/shopping/order");
        }
    }
}

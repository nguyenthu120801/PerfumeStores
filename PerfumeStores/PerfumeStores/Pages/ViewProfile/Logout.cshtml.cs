using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Pages.Shared;

namespace PerfumeStores.Pages.ViewProfile
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            LayoutModel.AmountCart = 0;
            LayoutModel.UserName = null;
            LoginDTO.CustomerID = null;
            LoginDTO.IsAdmin = false;
            return Redirect("/index");
        }
    }
}

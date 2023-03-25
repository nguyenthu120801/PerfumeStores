using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Services;
using PerfumeStores.Pages.Shared;

namespace PerfumeStores.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }
        [BindProperty]
        public LoginDTO LoginDTO { get; set; }

        [BindProperty]
        public RegisterDTO RegisterDTO { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogin()
        {
            try
            {
                var loginInfo = await _authService.Login(LoginDTO);
                if (loginInfo != null)
                {
                    if (loginInfo.IsAdmin)
                    {
                        LoginDTO.IsAdmin = true;
                        LayoutModel.UserName = loginInfo.Username;
                    }
                    LoginDTO.CustomerID = loginInfo.CustomerId;
                    //HttpContext.Session.SetString("Customer", JsonSerializer.Serialize<Customer>(loginInfo));
                    return this.RedirectToPage("./index");
                }
                else
                {
                    ViewData["Error"] = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return this.Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            try
            {
                var result = await _authService.Register(RegisterDTO);
                if (result)
                {
                    LoginDTO = new LoginDTO() { Username = RegisterDTO.Username, Password = RegisterDTO.Password };
                    var loginInfo = await _authService.Login(LoginDTO);
                    LoginDTO.CustomerID = loginInfo.CustomerId;
                    //HttpContext.Session.SetString("Customer", JsonSerializer.Serialize<Customer>(loginInfo));
                    return this.RedirectToPage("./index");
                }
                else
                {
                    ViewData["ErrorRegis"] = "Account has already";
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return this.Page();
        }
    }
}

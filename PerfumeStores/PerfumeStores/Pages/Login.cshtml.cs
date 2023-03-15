using AutoMapper;
using Common.Securities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Services;
using PerfumeStores.Services.Services;

namespace PerfumeStores.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LoginModel(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
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
                    LoginDTO = _mapper.Map<LoginDTO>(RegisterDTO);
                    var loginInfo = await _authService.Login(LoginDTO);
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

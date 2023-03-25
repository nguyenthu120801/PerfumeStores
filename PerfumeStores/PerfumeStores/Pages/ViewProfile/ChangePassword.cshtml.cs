using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStores.Pages.ViewProfile
{
    public class ChangePasswordModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;

        public ChangePasswordModel(PerfumeStores.Core.Models.Prn221Context context)
        {
            _context = context;
        }
        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu cũ.")]
        public string OldPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu mới.")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu mới không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public Customer cus { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }


            if (customer == null)
            {
                return NotFound();
            }
            cus = customer;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            cus = customer;
            if (cus.Password != OldPassword)
            {
                ViewData["Message"] = "Mật khẩu cũ không chính xác. Xin hãy nhập lại";

            }
            else if(cus.Password == NewPassword)
            {
                ViewData["Message"] = "Mật khẩu mới phải khác mật khẩu cũ. Xin hãy nhập lại";
            }
            else if(NewPassword != ConfirmPassword)
            {

            }
            else 
            {
                cus.Password = NewPassword;
                _context.Attach(cus).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                ViewData["Message"] = "Đổi mật khẩu thành công.";
            }
            return Page();
        }
    }
}

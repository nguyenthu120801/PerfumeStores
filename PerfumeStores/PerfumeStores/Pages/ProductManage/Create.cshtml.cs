using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PerfumeStores.Core.Models;
using PerfumeStores.Services;

namespace PerfumeStores.Pages.ProductManage
{
    public class CreateModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;
        private readonly ImageHandle imageHandle = new ImageHandle();
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public CreateModel(PerfumeStores.Core.Models.Prn221Context context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [Required(ErrorMessage = "Choose one File!")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,jpe,bmp,gif,png")]
        [BindProperty]
        public IFormFile Image { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Product.Image = imageHandle.AddImage(Image, _environment);
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

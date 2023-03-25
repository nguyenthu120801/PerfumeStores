using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;
using PerfumeStores.Services;
using System.ComponentModel.DataAnnotations;

namespace PerfumeStores.Pages.ProductManage
{
    public class EditModel : PageModel
    {
        private readonly PerfumeStores.Core.Models.Prn221Context _context;
        private readonly IImageHandle _imageHandle;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public EditModel(PerfumeStores.Core.Models.Prn221Context context, IImageHandle imageHandle, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _imageHandle = imageHandle;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        [Required(ErrorMessage = "Chọn một file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg,jpeg,jpe,bmp,gif,png")]
        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product =  await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Product.Image = await _imageHandle.AddImage(Image, _environment);
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}

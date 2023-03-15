using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;

namespace PerfumeStores.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly Prn221Context _context;
        public ProductService(Prn221Context context) => _context = context;


        public async Task<PaginatedList<Product>> ProductPaging(int cateId, int index, string searchString)
        {
            var list = new List<Product>();
            if (cateId == 0)
            {
                list = await _context.Products.ToListAsync();
            }
            else
            {
                list = await _context.Products.Where(x => x.CategoryId == cateId).ToListAsync();
            }
            if (!searchString.IsNullOrEmpty()) list = list.Where(x => x.Name.Contains(searchString)).ToList();
            return await new PaginatedList<Product>(list, list.Count(), 1, 12).Create(list as IQueryable<Product>, index, 12);
        }
    }
}

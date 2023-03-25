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

        public async Task<List<Product>> ProductHome()
        {
            return await _context.Products.Skip(new Random().Next(0, (_context.Products.Count() - 8))).Take(8).ToListAsync();
        }

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
            var paging = PaginatedList<Product>.Create(list.AsQueryable<Product>(), index, 12);
            return paging;
        }

        public async Task<int> TotalPage(int cateId, int index, string searchString)
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
            return new PaginatedList<Product>(list, list.Count(), 1, 12).TotalPages;
        }

        public async Task<int> TotalFound(int cateId, int index, string searchString)
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
            return list.Count;
        }
    }
}

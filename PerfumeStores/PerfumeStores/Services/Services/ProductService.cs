using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;

namespace PerfumeStores.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly Prn221Context _context;
        public ProductService(Prn221Context context) => _context = context;


        public async Task<PaginatedList<Product>> ProductPaging(int index)
        {
            return await new PaginatedList<Product>(_context.Products.ToList(), _context.Products.Count(), 1, 4).Create(_context.Products.ToList() as IQueryable<Product>, index, 4);
        }
    }
}

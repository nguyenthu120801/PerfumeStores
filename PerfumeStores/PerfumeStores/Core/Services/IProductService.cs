using PerfumeStores.Core.Models;
using PerfumeStores.Services.Services;

namespace PerfumeStores.Core.Services
{
    public interface IProductService
    {
        Task<PaginatedList<Product>> ProductPaging(int index);
    }
}

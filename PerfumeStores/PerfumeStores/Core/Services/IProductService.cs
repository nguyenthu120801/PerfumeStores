using PerfumeStores.Core.Models;
using PerfumeStores.Services.Services;

namespace PerfumeStores.Core.Services
{
    public interface IProductService
    {
        Task<PaginatedList<Product>> ProductPaging(int cateId, int index, string searchString);

        Task<List<Product>> ProductHome();

        Task<int> TotalPage(int cateId, int index, string searchString);

        Task<int> TotalFound(int cateId, int index, string searchString);
    }
}

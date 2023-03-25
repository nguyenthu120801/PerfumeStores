using PerfumeStores.Core.Models;

namespace PerfumeStores.Core.Services
{
    public interface IShoppingCart
    {
        Task<List<Cart>> GetCartItems(int id);
        Task<int> RemoveFromCart(int proId, int id);
        Task AddToCart(int productId, int id);
        Task<decimal> GetTotal(int id);
        Task<int> CreateOrder(Order order, string address, int id);
    }
}

using PerfumeStores.Core.Models;

namespace PerfumeStores.Core.Services
{
    public interface IShoppingCart
    {
        Task<List<Cart>> GetCartItems();
        Task<int> RemoveFromCart(int id);
        Task AddToCart(int productId);
        Task<decimal> GetTotal();
        Task<int> CreateOrder(Order order, string address);
    }
}

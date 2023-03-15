using Microsoft.EntityFrameworkCore;
using PerfumeStores.Core.DTOs;
using PerfumeStores.Core.Models;
using PerfumeStores.Core.Services;

namespace PerfumeStores.Services.Services
{
    public partial class ShoppingCart : IShoppingCart
    {
        private readonly Prn221Context _context;

        public ShoppingCart(Prn221Context context)
        {
            _context = context;
        }

        public async Task AddToCart(int productId)
        {
            // Get the matching cart and album instances
            var cartItem = await _context.Carts.SingleOrDefaultAsync(
                c => c.CustomerId == LoginDTO.Id
                && c.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    CustomerId = (int)LoginDTO.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                await _context.Carts.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            await _context.SaveChangesAsync();
        }
        public async Task<int> RemoveFromCart(int id)
        {
            var cartItem = await _context.Carts.SingleOrDefaultAsync(
                c => c.CustomerId == LoginDTO.Id
                && c.ProductId == id);

            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    _context.Carts.Remove(cartItem);
                }
                // Save changes
                await _context.SaveChangesAsync();
            }
            return itemCount;
        }
        private async Task EmptyCart()
        {
            var cartItems = _context.Carts
                .Where(cart => cart.CartId == LoginDTO.Id);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }
            // Save changes
            await _context.SaveChangesAsync();
        }
        public async Task<List<Cart>> GetCartItems()
        {
            return await _context.Carts.Where(cart => cart.CartId == LoginDTO.Id)
                .Include(cart => cart.Product)
                .ToListAsync();
        }
        public async Task<int> GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = await (from cartItems in _context.Carts
                          where cartItems.CartId == LoginDTO.Id
                          select (int?)cartItems.Quantity).SumAsync();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public async Task<decimal> GetTotal()
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total

            decimal? total = (decimal?) await (from cartItems in _context.Carts
                                        where cartItems.CartId == LoginDTO.Id
                                        select (int?)cartItems.Quantity * cartItems.Product.Price).SumAsync();
            return total ?? 0;
        }
        public async Task<int> CreateOrder(Order order, string address)
        {
            decimal orderTotal = 0;
            var cartItems = await GetCartItems();
            foreach (var item in cartItems)
                //orderTotal += (item.Quantity * item.Product.Price);
                try
                {
                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    return -1;
                }
            int orderID = _context.Orders.Select(o => o.OrderId).Max();
            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderStatus = "Chờ xác nhận",
                    OrderId = orderID,
                    ProductId = item.ProductId,
                    ShippingAddress = address,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Product.Price
                };
                try
                {
                    //_context.OrderDetails.Add(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    return -1;
                }
            }
            // Empty the shopping cart
            await EmptyCart();
            // Return the OrderId as the confirmation number
            return orderID;
        }
    }
}

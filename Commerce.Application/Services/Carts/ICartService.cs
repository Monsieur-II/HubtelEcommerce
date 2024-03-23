using Commerce.Application.DTOs.Cart;
using Commerce.Infras.Models;

namespace Commerce.Application.Services.Carts;

public interface ICartService
{
    Task<Cart?> AddItemToCart(CartItemDto cartItemDto, string phoneNumber);
    Task<Cart?> GetCartById(Guid id, string phoneNumber);
    Task<bool> RemoveItemById(Guid id, string phoneNumber);
    
    Task<IEnumerable<Cart>> GetCarts(CartFilterDto cartFilterDto);
}

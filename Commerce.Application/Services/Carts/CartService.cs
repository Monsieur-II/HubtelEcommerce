using Commerce.Application.DTOs.Cart;
using Commerce.Infras.Data;
using Commerce.Infras.Models;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Application.Services.Carts;

public class CartService : ICartService
{
    private readonly CommerceDbContext _context;

    public CartService(CommerceDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> AddItemToCart(CartItemDto cartItemDto, string phoneNumber)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber && c.ItemId == cartItemDto.ItemId);  
        if (cart == null)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == cartItemDto.ItemId);
            
            if (item == null) return null;
            
            var newCart = new Cart
            {
                PhoneNumber = phoneNumber,
                ItemId = item.Id,
                ItemName = item.Name,
                Quantity = cartItemDto.Quantity > 0 ? cartItemDto.Quantity : 1,
                UnitPrice = item.UnitPrice,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };

            await _context.AddAsync(newCart);
            await _context.SaveChangesAsync();

            return newCart;

        }
        else
        {
            cart.Quantity += cartItemDto.Quantity > 0 ? cartItemDto.Quantity : 0;
            cart.DateModified = DateTime.UtcNow;
            _context.Update(cart);
           await _context.SaveChangesAsync();
           return cart;
        }
    }

    public async Task<Cart?> GetCartById(Guid id, string phoneNumber)
    {
        var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber && c.ItemId == id);

        return cartItem ?? null;
    }

    public async Task<bool> RemoveItemById(Guid id, string phoneNumber)
    {
        var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber && c.ItemId == id);

        if (cartItem == null) return false;
        
        _context.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;

    }

    public async Task<IEnumerable<Cart>> GetCarts(CartFilterDto cartFilterDto)
    {
        var carts = _context.Carts.AsQueryable();

        if (!string.IsNullOrEmpty(cartFilterDto.ItemName))
            carts = carts.Where(c => c.ItemName == cartFilterDto.ItemName);

        if (cartFilterDto.PhoneNumbers?.Any() == true)
        {
            carts = carts.Where(cart => cartFilterDto.PhoneNumbers.Contains(cart.PhoneNumber));
        }

        if (!string.IsNullOrEmpty(cartFilterDto.Time) && DateTime.TryParse(cartFilterDto.Time, out var time))
            carts = carts.Where(c => c.DateModified <= time);

        if (cartFilterDto.Quantity >= 1)
            carts = carts.Where(c => c.Quantity >= cartFilterDto.Quantity);

        var response = await carts.ToListAsync();

        return response;

    }
}

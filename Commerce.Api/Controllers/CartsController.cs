using Commerce.Application.DTOs.Cart;
using Commerce.Application.Services.Carts;
using Commerce.Application.Utils;
using Commerce.Infras.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Api.Controllers;

[ApiController]
[Route("api/v1/carts")]
[Authorize]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CartResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddItemToCart(CartItemDto cartItemDto)
    {
        if (cartItemDto.Quantity < 0)
            return BadRequest(new Response()
            {
                Message = "Negative values not allowed for quantity",
                status = "Error",
                Success = false,
            });
        
        var userName = User.Identity?.Name;
        
        var cart = await _cartService.AddItemToCart(cartItemDto, userName);
        if (cart == null)
            return BadRequest("Item does not exist");
        
        return CreatedAtAction(nameof(GetCartById), new { id = cart.ItemId }, MapCartToResponse(cart));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CartResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCartById(Guid id)
    {
        var userName = User.Identity?.Name;
        
        var cart = await _cartService.GetCartById(id, userName);
        if (cart == null)
            return NotFound("Cart not found.");

        var cartResponse = MapCartToResponse(cart);
        
        return Ok(cartResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CartResponseDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCarts([FromBody] CartFilterDto cartFilterDto)
    {
        var carts = await _cartService.GetCarts(cartFilterDto);
        
        var cartResponses = carts.Select(MapCartToResponse);
        
        return Ok(cartResponses);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveItemById(Guid id)
    {
        var userName = User.Identity?.Name;
        
        var result = await _cartService.RemoveItemById(id, userName);
        if (!result)
            return NotFound("No item found in cart.");
        
        return NoContent();
    }
    
    private CartResponseDto MapCartToResponse(Cart cart)
    {
        return new CartResponseDto
        {
            ItemName = cart.ItemName,
            UnitPrice = cart.UnitPrice.ToString("0.#0"),
            Quantity = cart.Quantity,
            TotalPrice = (cart.UnitPrice * cart.Quantity).ToString("0.#0")
        };
    }
    
}

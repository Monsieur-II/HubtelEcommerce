using System.ComponentModel.DataAnnotations;

namespace Commerce.Application.DTOs.Cart;

public class CartItemDto
{
    [Required(ErrorMessage = "Item id is required")]
    public Guid ItemId { get; set; }
    
    [Required(ErrorMessage = "Item quantity is required")]
    public int Quantity { get; set; }
}

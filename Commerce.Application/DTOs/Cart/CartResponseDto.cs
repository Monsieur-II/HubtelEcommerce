namespace Commerce.Application.DTOs.Cart;

public class CartResponseDto
{
    public string ItemName { get; set; }
    public string UnitPrice { get; set; }
    public int Quantity { get; set; }
    public string TotalPrice { get; set; }
}

namespace Commerce.Application.DTOs.Cart;

public class CartFilterDto
{
    public List<string> PhoneNumbers { get; set; }
    public string Time { get; set; }
    public int Quantity { get; set; }
    public string ItemName { get; set; }
}

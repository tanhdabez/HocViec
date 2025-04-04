namespace Core.Request
{
    public class CartItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public decimal Price { get; set; }
    }
  
}

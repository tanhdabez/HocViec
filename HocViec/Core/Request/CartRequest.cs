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
    public class CartResult
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public int CartItemCount { get; set; }
    }
}

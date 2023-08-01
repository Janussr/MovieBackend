namespace MovieBackend.DTOs
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int MovieId { get; set; }
        public int Quantity { get; set; }
    }

}

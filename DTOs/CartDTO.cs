namespace MovieBackend.DTOs
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public required List<CartItemDTO> CartItems { get; set; }
    }

}

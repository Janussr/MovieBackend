namespace MovieBackend.DTOs
{
    public class CartDTO
    {

        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Quantity { get; set; }
    }
}

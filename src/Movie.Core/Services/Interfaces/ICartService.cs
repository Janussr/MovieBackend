namespace Movies.Core.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddMovieToCart(int movieId, int userId, int quantity = 1);

        Task<bool> PurchaseCartItems(int userId);
    }
}

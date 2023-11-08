using HITSBackEnd.Dto.CartDTO;

namespace HITSBackEnd.Services.UserCart
{
    public interface IUserCartRepository
    {
        public Task AddDishToCart(Guid id, string email);
        public Task RemoveDishFromCart(string email, Guid id, bool increase);
        public Task<GetCartDTO> GetUserCart(string email);
    }
}

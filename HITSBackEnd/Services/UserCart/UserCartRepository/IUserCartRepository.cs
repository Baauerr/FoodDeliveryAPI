using HITSBackEnd.Dto.CartDTO;

namespace HITSBackEnd.Services.UserCart.UserCartRepository
{
    public interface IUserCartRepository
    {
        public Task AddDishToCart(string id, string email);
        public Task RemoveDishFromCart(string email, string id, bool increase);
        public GetCartDTO GetUserCart(string email);
    }
}

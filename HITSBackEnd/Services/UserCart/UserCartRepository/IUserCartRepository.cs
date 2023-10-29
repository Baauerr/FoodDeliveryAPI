using HITSBackEnd.Dto.CartDTO;

namespace HITSBackEnd.Services.UserCart.UserCartRepository.UserCartRepository
{
    public interface IUserCartRepository
    {
        public void AddDishToCart(string id, string email);
        public void RemoveDishFromCart(string email, string id, bool increase);
        public GetCartDTO GetUserCart(string email);
    }
}

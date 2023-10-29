using HITSBackEnd.Dto.CartDTO;

namespace HITSBackEnd.Services.UserCart
{
    public interface IUserCartRepository
    {
        public void AddDishToCart(string id, string email);
        public void RemoveDishFromCart();
        public GetCartDTO GetUserCart(string email);
    }
}

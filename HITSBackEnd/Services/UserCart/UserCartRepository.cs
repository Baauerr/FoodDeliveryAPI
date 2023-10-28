using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.CartDTO;

namespace HITSBackEnd.Services.UserCart
{
    public class UserCartRepository : IUserCartRepository
    {
        private readonly AppDbContext _db;

        public UserCartRepository(AppDbContext dbContext) {
            _db = dbContext;
        }
        public void AddDishToCart(string id, string email)
        {
            if (_db.Carts.Any(d => d.DishId == id))
            {
                var dish = _db.Carts.FirstOrDefault(d => d.DishId == id);
                dish.AmountOfDish += 1;
                _db.SaveChanges();
            }
            else
            {
                Cart cart = new Cart();
                cart.DishId = id;
                cart.UserEmail = email;
                cart.AmountOfDish = 1;
                _db.Add(cart);
                _db.SaveChanges();
            }
        }
        public UserCartDTO GetUserCart()
        {
            throw new NotImplementedException();
        }
        public void RemoveDishFromCart()
        {
            throw new NotImplementedException();
        }
    }
}

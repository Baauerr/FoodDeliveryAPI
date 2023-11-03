using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.CartDTO;
using HITSBackEnd.Services.Dishes.DishesRepository;
using HITSBackEnd.Swagger;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Services.UserCart.UserCartRepository
{
    public class UserCartRepository : IUserCartRepository
    {
        private readonly AppDbContext _db;

        public UserCartRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task AddDishToCart(string id, string email)
        {
            if (_db.Carts.Any(d => d.DishId == id))
            {
                var dish = await _db.Carts.FirstOrDefaultAsync(d => d.DishId == id);
                dish.AmountOfDish += 1;
                await _db.SaveChangesAsync();
            }
            else
            {
                CartTable cart = new CartTable();
                cart.DishId = id;
                cart.UserEmail = email;
                cart.AmountOfDish = 1;
                await _db.AddAsync(cart);
                await _db.SaveChangesAsync();
            }
        }
        public GetCartDTO GetUserCart(string email)
        {
            var userCartItems = _db.Carts
            .Where(cart => cart.UserEmail == email)
            .Join(
                _db.Dishes,
                cart => cart.DishId,
                dish => dish.Id.ToString(),
                (cart, dish) => new UserCartDTO
                {
                    Id = cart.DishId.ToString(),
                    Name = dish.Name,
                    Price = dish.Price,
                    TotalPrice = cart.AmountOfDish * dish.Price,
                    Amount = cart.AmountOfDish,
                    Image = dish.Photo
                })
             .ToList();
            GetCartDTO cartDTO = new GetCartDTO();
            cartDTO.GetDishesDTO = userCartItems;
            return cartDTO;
        }


        public async Task RemoveDishFromCart(string email, string dishId, bool increase)
        {
            var cartItem = await _db.Carts.FirstOrDefaultAsync(cart => cart.UserEmail == email && cart.DishId == dishId);

            if (cartItem == null)
            {
                throw new Exception(ErrorCreator.CreateError("Такого блюда в корзине пользователя нет"));
            }

            if (!increase || cartItem.AmountOfDish == 1)
            {
                _db.Carts.Remove(cartItem);
            }
            else
            {
                cartItem.AmountOfDish -= 1;
            }
            await _db.SaveChangesAsync();
        }
    }
}

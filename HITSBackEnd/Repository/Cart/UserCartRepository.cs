using HITSBackEnd.DataBaseContext;
using HITSBackEnd.Dto.CartDTO;
using HITSBackEnd.Models.CartModels;
using HITSBackEnd.Services.UserCart;
using HITSBackEnd.Swagger;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Repository.UserCart
{
    public class UserCartRepository : IUserCartRepository
    {
        private readonly AppDbContext _db;

        public UserCartRepository(AppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task AddDishToCart(Guid id, string email)
        {
            if (!_db.Dishes.Any(d => d.Id == id)) {
                throw new NotFoundException("Такого блюда нет в меню");
            }

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
                dish => dish.Id,
                (cart, dish) => new UserCartDTO
                {
                    Id = cart.DishId,
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


        public async Task RemoveDishFromCart(string email, Guid dishId, bool increase)
        {
            var cartItem = await _db.Carts.FirstOrDefaultAsync(cart => cart.UserEmail == email && cart.DishId == dishId);

            if (cartItem == null)
            {
                throw new NotFoundException("Такого блюда в корзине пользователя нет");
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

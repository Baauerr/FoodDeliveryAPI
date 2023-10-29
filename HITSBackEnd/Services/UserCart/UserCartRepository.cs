﻿using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.CartDTO;
using Microsoft.EntityFrameworkCore;

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
       
        
        public void RemoveDishFromCart()
        {
            throw new NotImplementedException();
        }
    }
}

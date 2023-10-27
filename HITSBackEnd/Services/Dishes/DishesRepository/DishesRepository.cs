using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.DishDTO;
using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Services.Account.IRepository;
using HITSBackEnd.Swagger;
using System;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public class DishesRepository : IDishesRepository
    {

        private readonly AppDbContext _db;

        public DishesRepository(AppDbContext db)
        {
            _db = db;
        }
        public string GetListOfDishes()
        {
            throw new NotImplementedException();
        }

        public bool RatingCheck()
        {
            throw new NotImplementedException();
        }

        public DishResponseDTO GetConcretteDish(string id)
        {

            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid))
            {
                throw new Exception(ErrorCreator.CreateError("Такого блюда нет в меню"));
            }

            var dish = _db.Dishes.FirstOrDefault(u => u.Id == idGuid);

            DishResponseDTO response = new DishResponseDTO
                     {
                         Id = dish.Id,
                         Name = dish.Name,
                         Category = dish.Category,
                         Price = dish.Price,
                         Description = dish.Description,
                         IsVegeterian = dish.IsVegetarian,
                         Photo = dish.Photo
                      };

            return response;
        }
    }
}

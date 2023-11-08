using HITSBackEnd.Dto.DishDTO;
using HITSBackEnd.Models.DishesModels;

namespace HITSBackEnd.Services.Dishes
{
    public interface IDishesRepository
    {
        public Task<bool> RatingCheck(Guid dishId, string userEmail);
        public Task<DishTable> GetConcretteDish(Guid id);
        public DishPageResponseDTO GetDishesPage(List<Category> categories, bool? isVegetarian, SortingTypes sorting, int page);

        public Task SetRating(Guid dishId, string userEmail, double rate);
    }
}

using HITSBackEnd.Dto.DishDTO;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public interface IDishesRepository
    {
        public Task<bool> RatingCheck(string dishId, string userEmail);
        public Task<DishTable> GetConcretteDish(string id);
        public DishPageResponseDTO GetDishesPage(List<Category> categories, bool? isVegetarian, SortingTypes sorting, int page);

        public Task SetRating(string dishId, string userId, double rate);
    }
}

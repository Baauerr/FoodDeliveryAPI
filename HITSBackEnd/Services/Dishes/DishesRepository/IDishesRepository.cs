using HITSBackEnd.Dto.DishDTO;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public interface IDishesRepository
    {
        public bool RatingCheck(string dishId, string userEmail);
        public DishTable GetConcretteDish(string id);
        public DishPageResponseDTO GetDishesPage(List<Category> categories, bool? isVegetarian, SortingTypes sorting, int page);

        public void SetRating(string dishId, string userId, double rate);
    }
}

using HITSBackEnd.Dto.DishDTO;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public interface IDishesRepository
    {
        public bool RatingCheck();
        public DishResponseDTO GetConcretteDish(string id);
        public DishPageResponseDTO GetDishesPage(List<Category> categories, bool? isVegetarian, SortingTypes sorting, int page);

        public void SetRaiting(string dishId, string userId);
    }
}

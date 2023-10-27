using HITSBackEnd.Dto.DishDTO;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public interface IDishesRepository
    {
        public string GetListOfDishes();
        public bool RatingCheck();
        public DishResponseDTO GetConcretteDish(string id);
    }
}

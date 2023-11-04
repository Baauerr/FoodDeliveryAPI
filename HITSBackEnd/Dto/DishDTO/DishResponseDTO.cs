using HITSBackEnd.Services.Dishes.DishesRepository;

namespace HITSBackEnd.Dto.DishDTO
{
    public class DishResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsVegeterian { get; set; }
        public string Photo { get; set; }
    }
}

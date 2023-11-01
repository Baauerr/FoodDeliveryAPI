using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public class DishTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsVegetarian { get; set; }
        public string Photo { get; set; }
    }
}

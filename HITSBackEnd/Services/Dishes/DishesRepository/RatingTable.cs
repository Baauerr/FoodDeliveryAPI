using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public class RatingTable
    {
        [Key]
        public string DishId { get; set; }
        public int Value { get; set; }
    }
}

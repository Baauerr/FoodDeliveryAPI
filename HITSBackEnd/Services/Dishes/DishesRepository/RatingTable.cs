using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public class RatingTable
    {
        [Key]
        public string DishId { get; set; }
        [Key]
        public string UserEmail { get; set; }
        public double Value { get; set; }
    }
}

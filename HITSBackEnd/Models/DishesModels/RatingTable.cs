using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Models.DishesModels
{
    public class RatingTable
    {
        [Key]
        public Guid DishId { get; set; }
        [Key]
        public string UserEmail { get; set; }
        public double Value { get; set; }
    }
}

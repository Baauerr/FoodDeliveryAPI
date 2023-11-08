using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HITSBackEnd.Models.CartModels
{
    public class CartTable
    {
        [Key]
        [Column(Order = 0)]
        public string UserEmail { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid DishId { get; set; }
        public int AmountOfDish { get; set; }
    }
}

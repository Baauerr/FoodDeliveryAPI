using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HITSBackEnd.Services.UserCart
{
    public class Cart
    {
        [Key]
        [Column(Order = 0)]
        public string UserEmail {  get; set; }
        [Key]
        [Column(Order = 1)]
        public string DishId { get; set; }
        public int AmountOfDish { get; set; }
    }
}

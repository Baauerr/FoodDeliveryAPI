using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Services.UserCart
{
    public class Cart
    {
        [Key]
        public string UserEmail {  get; set; }
        public string DishId { get; set; }
        public int AmountOfDish { get; set; }
    }
}

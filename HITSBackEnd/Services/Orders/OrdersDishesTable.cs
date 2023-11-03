using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Services.Orders
{
    public class OrdersDishesTable
    {
        [Key]
        public string DishId { get; set; }
        [Key]
        public string OrderId { get; set; }
        public int Amount { get; set; }
    }
}

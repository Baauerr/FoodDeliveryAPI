using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Models.OrdersModels
{
    public class OrdersDishesTable
    {
        [Key]
        public Guid DishId { get; set; }
        [Key]
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
    }
}

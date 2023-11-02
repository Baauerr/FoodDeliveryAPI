using HITSBackEnd.Services.Orders;
using System.ComponentModel.DataAnnotations;

namespace HITSBackEnd.Dto.OrderDTO
{
    public class ConcretteOrderResponseDTO
    {     
        public Guid Id { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public Status Status { get; set; }
        public double Price { get; set; }
        public List<DishInOrderDTO>? DishInOrder { get; set; }
        public string Adress { get; set; }
    }
}

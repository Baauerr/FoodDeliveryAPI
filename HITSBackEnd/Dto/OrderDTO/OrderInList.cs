using HITSBackEnd.Models.OrdersModels;

namespace HITSBackEnd.Dto.OrderDTO
{
    public class OrderInList
    {
        public Guid Id { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public Status Status { get; set; }
        public double Price {  get; set; }
    }
}

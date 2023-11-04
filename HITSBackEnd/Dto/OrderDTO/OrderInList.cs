using HITSBackEnd.Services.Orders;

namespace HITSBackEnd.Dto.OrderDTO
{
    public class OrderInList
    {
        public string Id { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public Status Status { get; set; }
        public double Price {  get; set; }
    }
}

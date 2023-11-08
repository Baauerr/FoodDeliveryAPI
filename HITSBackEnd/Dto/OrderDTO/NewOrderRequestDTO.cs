namespace HITSBackEnd.Dto.OrderDTO
{
    public class NewOrderRequestDTO
    {
        public DateTime DeliveryTime { get; set; }
        public Guid addressId { get; set; }
    }
}

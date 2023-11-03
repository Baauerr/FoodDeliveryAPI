using HITSBackEnd.Dto.OrderDTO;

namespace HITSBackEnd.Services.Orders
{
    public interface IOrdersRepository
    {
        public ConcretteOrderResponseDTO GetConcretteOrder(string orderId);
        public List<OrderInList> GetListOfOrders(string userId);
        public void CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string userId);
        public void ConfirmOrderDelivery(string orderId);
    }
}

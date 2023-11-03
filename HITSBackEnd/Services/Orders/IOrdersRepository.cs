using HITSBackEnd.Dto.OrderDTO;

namespace HITSBackEnd.Services.Orders
{
    public interface IOrdersRepository
    {
        public Task<ConcretteOrderResponseDTO> GetConcretteOrder(string orderId);
        public List<OrderInList> GetListOfOrders(string userId);
        public Task CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string userId);
        public Task ConfirmOrderDelivery(string orderId);
    }
}

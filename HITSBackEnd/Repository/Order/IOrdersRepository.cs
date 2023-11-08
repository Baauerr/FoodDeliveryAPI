using HITSBackEnd.Dto.OrderDTO;

namespace HITSBackEnd.Services.Orders
{
    public interface IOrdersRepository
    {
        public Task<ConcretteOrderResponseDTO> GetConcretteOrder(Guid orderId);
        public List<OrderInList> GetListOfOrders(string userEmail);
        public Task CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string userEmail);
        public Task ConfirmOrderDelivery(Guid orderId, string userEmail);
    }
}

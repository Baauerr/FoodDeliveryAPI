using HITSBackEnd.Dto.OrderDTO;

namespace HITSBackEnd.Services.Order
{
    public interface IOrdersRepository
    {
        public Task<ConcretteOrderResponseDTO> GetConcretteOrder(Guid orderId);
        public Task<List<OrderInListDTO>> GetListOfOrders(string userEmail);
        public Task CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string userEmail);
        public Task ConfirmOrderDelivery(Guid orderId, string userEmail);
    }
}

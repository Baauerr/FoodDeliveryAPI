using HITSBackEnd.Dto.OrderDTO;

namespace HITSBackEnd.Services.Orders
{
    public interface IOrdersRepository
    {
        public ConcretteOrderResponseDTO GetConcretteOrder(string id);
        public ListOfOrdersResponseDTO GetListOfOrders();
        public void CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string UserId);
        public void ConfirmOrderDelivery(string id);

        private void ConnectDishesToOrder(string userEmail) { }
    }
}

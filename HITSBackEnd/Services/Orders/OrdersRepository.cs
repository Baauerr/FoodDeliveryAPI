using HITSBackEnd.Dto.OrderDTO;

namespace HITSBackEnd.Services.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        public void CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO)
        {
            throw new NotImplementedException();
        }

        public ConcretteOrderResponseDTO GetConcretteOrder(string id)
        {
            throw new NotImplementedException();
        }

        public ListOfOrdersResponseDTO GetListOfOrders()
        {
            throw new NotImplementedException();
        }

        public void ConfirmOrderDelivery(string id)
        {
            throw new NotImplementedException();
        }
    }
}

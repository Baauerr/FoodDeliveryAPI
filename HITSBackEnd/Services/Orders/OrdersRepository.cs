using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.OrderDTO;

namespace HITSBackEnd.Services.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private AppDbContext _db;

        public OrdersRepository(AppDbContext appDbContext) {
            _db = appDbContext;
        }
        public void CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string userEmail)
        {
            var newOrder = new OrdersTable()
            {
                UserEmail = userEmail,
                DeliveryTime = newOrderRequestDTO.DeliveryTime,
                OrderTime = DateTime.Now.ToUniversalTime(),
                Status = Status.inProcess,
                Price = -1,
                Adress = newOrderRequestDTO.addressId
            };

            _db.Orders.Add(newOrder);
            _db.SaveChanges();
            ConnectDishesToOrder(userEmail);

        }

        public ConcretteOrderResponseDTO GetConcretteOrder(string orderId)
        {
            var dishesInOrder = _db.OrdersDishes.Where(order => order.OrderId == orderId).ToList();

            List<DishInOrderDTO> orderDishes = new List<DishInOrderDTO>();

            foreach (var dish in dishesInOrder)
            {
                var dishFromDb = _db.Dishes.FirstOrDefault(d => d.Id == Guid.Parse(dish.DishId));
                DishInOrderDTO dishInOrder = new DishInOrderDTO
                {
                    Id = dishFromDb.Id.ToString(),
                    Name = dishFromDb.Name,
                    price = dishFromDb.Price,
                    TotalPrice = dish.Amount * dishFromDb.Price,
                    Amount = dish.Amount,
                    Image = dishFromDb.Photo    
                };

                orderDishes.Add(dishInOrder);

            }

            var concretteOrderFromDb = _db.Orders.FirstOrDefault(order => order.Id == Guid.Parse(orderId));
            ConcretteOrderResponseDTO concretteOrder = new ConcretteOrderResponseDTO()
            {
                Id = concretteOrderFromDb.Id,
                DeliveryTime = concretteOrderFromDb.DeliveryTime,
                OrderTime = concretteOrderFromDb.OrderTime,
                Status = concretteOrderFromDb.Status,
                Price = concretteOrderFromDb.Price,
                DishInOrder = orderDishes,
                Adress = concretteOrderFromDb.Adress,
            };
            return concretteOrder;
        }

        public List<OrderInList> GetListOfOrders(string userEmail)
        {
            var allOrders = _db.Orders.Where(order => order.UserEmail == userEmail).ToList();
            List<OrderInList> listOfOrders = new List<OrderInList>();

            foreach (var order in allOrders)
            {
                OrderInList orderInList = new OrderInList
                {
                    Id = (order.Id).ToString(),
                    DeliveryTime = order.DeliveryTime,
                    OrderTime = order.OrderTime,
                    Status = order.Status,
                    Price = order.Price
                };

                listOfOrders.Add(orderInList);

            }
            return listOfOrders;
        }

        public void ConfirmOrderDelivery(string orderId)
        {
            var order = _db.Orders.FirstOrDefault(o => o.Id == Guid.Parse(orderId));
            order.Status = Status.Delivered;
            _db.SaveChanges();
        }

        private void ConnectDishesToOrder(string userEmail)
        {
            var cartItemsToMove = _db.Carts.Where(ct => ct.UserEmail == userEmail).ToList();

            _db.Carts.RemoveRange(cartItemsToMove);
            _db.SaveChanges();
            var Price = 0.0;
            var orderIdToAssign = GetOrderIdFromOrdersTableWithPriceMinusOne();
            var ordersDishesItems = cartItemsToMove.Select(cartItem => new OrdersDishesTable
            {
                DishId = cartItem.DishId,
                OrderId = orderIdToAssign,
                Amount = cartItem.AmountOfDish
            }).ToList();

            _db.OrdersDishes.AddRange(ordersDishesItems);
            _db.SaveChanges();
            AddPriceToOrder(orderIdToAssign);
        }
        private string GetOrderIdFromOrdersTableWithPriceMinusOne()
        {
            var order = _db.Orders.FirstOrDefault(ot => ot.Price == -1);
            if (order != null)
            {
                return order.Id.ToString();
            }

            return ""; 
        }

        private void AddPriceToOrder(string orderId)
        {
            var cartItemsToMove = _db.OrdersDishes.Where(ct => ct.OrderId == orderId).ToList();
            var totalPrice = 0.0;
            foreach (var cartItem in cartItemsToMove)
            {
                string dishId = cartItem.DishId;

                var dish = _db.Dishes.FirstOrDefault(d => d.Id ==  Guid.Parse(dishId));

                if (dish != null)
                {
                    int amountOfDish = cartItem.Amount;
                    double itemTotalPrice = dish.Price * amountOfDish;
                    totalPrice += itemTotalPrice;
                }
            }
            var orderToChange = _db.Orders.FirstOrDefault(ct => ct.Id == Guid.Parse(orderId));
            orderToChange.Price = totalPrice;
            _db.SaveChanges();
        }
    }
}

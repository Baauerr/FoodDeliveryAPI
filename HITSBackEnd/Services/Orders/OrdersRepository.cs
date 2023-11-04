using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.OrderDTO;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Services.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private AppDbContext _db;

        public OrdersRepository(AppDbContext appDbContext) {
            _db = appDbContext;
        }
        public async Task CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string userEmail)
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

            await _db.Orders.AddAsync(newOrder);
            await _db.SaveChangesAsync();
            await ConnectDishesToOrder(userEmail);

        }

        public async Task<ConcretteOrderResponseDTO> GetConcretteOrder(string orderId)
        {
            var dishesInOrder =  _db.OrdersDishes.Where(order => order.OrderId == orderId).ToList();

            List<DishInOrderDTO> orderDishes = new List<DishInOrderDTO>();

            foreach (var dish in dishesInOrder)
            {
                var dishFromDb = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == Guid.Parse(dish.DishId));
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

            var concretteOrderFromDb = await _db.Orders.FirstOrDefaultAsync(order => order.Id == Guid.Parse(orderId));
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

        public async Task ConfirmOrderDelivery(string orderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == Guid.Parse(orderId));
            order.Status = Status.Delivered;
            await _db.SaveChangesAsync();
        }

        private async Task ConnectDishesToOrder(string userEmail)
        {
            var cartItemsToMove = _db.Carts.Where(ct => ct.UserEmail == userEmail).ToList();

            _db.Carts.RemoveRange(cartItemsToMove);
            _db.SaveChanges();
            var Price = 0.0;
            var orderIdToAssign = await GetOrderIdFromOrdersTableWithPriceMinusOne();
            var ordersDishesItems = cartItemsToMove.Select(cartItem => new OrdersDishesTable
            {
                DishId = cartItem.DishId,
                OrderId = orderIdToAssign,
                Amount = cartItem.AmountOfDish
            }).ToList();

            await _db.OrdersDishes.AddRangeAsync(ordersDishesItems);
            await _db.SaveChangesAsync();
            await AddPriceToOrder(orderIdToAssign);
        }
        private async Task<string> GetOrderIdFromOrdersTableWithPriceMinusOne()
        {
            var order = await _db.Orders.FirstOrDefaultAsync(ot => ot.Price == -1);
            if (order != null)
            {
                return order.Id.ToString();
            }

            return ""; 
        }

        private async Task AddPriceToOrder(string orderId)
        {
            var cartItemsToMove = _db.OrdersDishes.Where(ct => ct.OrderId == orderId).ToList();
            var totalPrice = 0.0;
            foreach (var cartItem in cartItemsToMove)
            {
                string dishId = cartItem.DishId;

                var dish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id ==  Guid.Parse(dishId));

                if (dish != null)
                {
                    int amountOfDish = cartItem.Amount;
                    double itemTotalPrice = dish.Price * amountOfDish;
                    totalPrice += itemTotalPrice;
                }
            }
            var orderToChange = await _db.Orders.FirstOrDefaultAsync(ct => ct.Id == Guid.Parse(orderId));
            orderToChange.Price = totalPrice;
            await _db.SaveChangesAsync();
        }
    }
}

using HITSBackEnd.DataBaseContext;
using HITSBackEnd.DataValidation;
using HITSBackEnd.Dto.OrderDTO;
using HITSBackEnd.Models.OrdersModels;
using HITSBackEnd.Services.Adresses;
using HITSBackEnd.Swagger;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Services.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private AppDbContext _db;
        private DeliveryTimeChecker _timeChecker;
        private AddressValidator _addressValidator;

        public OrdersRepository(AppDbContext appDbContext, DeliveryTimeChecker timeChecker, AddressValidator addressValidator)
        {
            _db = appDbContext;
            _timeChecker = timeChecker;
            _addressValidator = addressValidator;
        }
        public async Task CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO, string userEmail)
        {
            var isUserCartNotZero = _db.Carts.Any(cart => cart.UserEmail == userEmail);

            if (!isUserCartNotZero)
            {
                throw new NotFoundException("Корзина пользователя пуста");
            }

            if (!_timeChecker.ValidTime(newOrderRequestDTO.DeliveryTime))
            {
                throw new BadRequestException("Недостаточно времени для заказа");
            }

            if (!_addressValidator.isAddressExist(newOrderRequestDTO.addressId))
            {
                throw new NotFoundException("Адресс не найден");
            }

            var newOrder = new OrdersTable()
            {
                Id = Guid.NewGuid(),
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

        public async Task<ConcretteOrderResponseDTO> GetConcretteOrder(Guid orderId)
        {
            var concretteOrderFromDb = await _db.Orders.FirstOrDefaultAsync(order => order.Id == orderId);

            if (concretteOrderFromDb == null)
            {
                throw new NotFoundException("Заказа с таким ID не существует");
            }

            ConcretteOrderResponseDTO concretteOrder = new ConcretteOrderResponseDTO()
            {
                Id = concretteOrderFromDb.Id,
                DeliveryTime = concretteOrderFromDb.DeliveryTime,
                OrderTime = concretteOrderFromDb.OrderTime,
                Status = concretteOrderFromDb.Status,
                Price = concretteOrderFromDb.Price,
                Adress = concretteOrderFromDb.Adress,
            };

            var dishesInOrder =  _db.OrdersDishes.Where(order => order.OrderId == orderId).ToList();

            List<DishInOrderDTO> orderDishes = new List<DishInOrderDTO>();

            foreach (var dish in dishesInOrder)
            {
                var dishFromDb = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == dish.DishId);
                DishInOrderDTO dishInOrder = new DishInOrderDTO
                {
                    Id = dishFromDb.Id,
                    Name = dishFromDb.Name,
                    price = dishFromDb.Price,
                    TotalPrice = dish.Amount * dishFromDb.Price,
                    Amount = dish.Amount,
                    Image = dishFromDb.Photo    
                };

                concretteOrder.DishInOrder = orderDishes;

                orderDishes.Add(dishInOrder);

            }

            return concretteOrder;
        }

        public List<OrderInList> GetListOfOrders(string userEmail)
        {
            var allOrders = _db.Orders.Where(order => order.UserEmail == userEmail).ToList();

            if (allOrders.Count == 0)
            {
                throw new NotFoundException("У пользователя нет заказов");
            }

            List<OrderInList> listOfOrders = new List<OrderInList>();

            foreach (var order in allOrders)
            {
                OrderInList orderInList = new OrderInList
                {
                    Id = order.Id,
                    DeliveryTime = order.DeliveryTime,
                    OrderTime = order.OrderTime,
                    Status = order.Status,
                    Price = order.Price
                };

                listOfOrders.Add(orderInList);

            }
            return listOfOrders;
        }

        public async Task ConfirmOrderDelivery(Guid orderId, string userEmail)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.UserEmail == userEmail);

            if (order == null)
            {
                throw new NotFoundException("У пользователя нет такого заказа");
            }
            if (order.Status == Status.Delivered)
            {
                throw new BadRequestException("Заказ уже подтверждён");
            }

            order.Status = Status.Delivered;
            await _db.SaveChangesAsync();
        }

        private async Task ConnectDishesToOrder(string userEmail)
        {
            var cartItemsToMove = _db.Carts.Where(ct => ct.UserEmail == userEmail).ToList();

            _db.Carts.RemoveRange(cartItemsToMove);
            _db.SaveChanges();
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
        private async Task<Guid> GetOrderIdFromOrdersTableWithPriceMinusOne()
        {
            var order = await _db.Orders.FirstOrDefaultAsync(ot => ot.Price == -1);
            if (order != null)
            {
                return order.Id;
            }

            return Guid.Empty;
        }

        private async Task AddPriceToOrder(Guid orderId)
        {
            var cartItemsToMove = _db.OrdersDishes.Where(ct => ct.OrderId == orderId).ToList();
            var totalPrice = 0.0;
            foreach (var cartItem in cartItemsToMove)
            {
                Guid dishId = cartItem.DishId;

                var dish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id ==  dishId);

                if (dish != null)
                {
                    int amountOfDish = cartItem.Amount;
                    double itemTotalPrice = dish.Price * amountOfDish;
                    totalPrice += itemTotalPrice;
                }
            }
            var orderToChange = await _db.Orders.FirstOrDefaultAsync(ct => ct.Id == orderId);
            orderToChange.Price = totalPrice;
            await _db.SaveChangesAsync();
        }
    }
}

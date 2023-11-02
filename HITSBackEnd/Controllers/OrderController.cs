using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Dto.OrderDTO;
using HITSBackEnd.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderController (IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        [HttpGet("{id}")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public IActionResult GetConcretteOrder(string id)
        {
            var concretteOrderResponseDTO = _ordersRepository.GetConcretteOrder(id);
            return Ok(concretteOrderResponseDTO);
        }

        [HttpGet("")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public IActionResult GetListOfOrders()
        {
            var orderResponseDTO = _ordersRepository.GetListOfOrders();
            return Ok(orderResponseDTO);
        }
        [HttpPost("")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public IActionResult CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO)
        {
            var userId = User.Identity.Name;
            _ordersRepository.CreateNewOrder(newOrderRequestDTO, userId);
            return Ok();
        }
        [HttpPost("{id}/status")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public IActionResult ConfirmOrderDelivery(string id)
        {
            _ordersRepository.ConfirmOrderDelivery(id);
            return Ok();
        }

    }
}

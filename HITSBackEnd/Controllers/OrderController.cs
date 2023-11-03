using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Dto.OrderDTO;
using HITSBackEnd.Services.Orders;
using HITSBackEnd.Swagger;
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
        public async Task<IActionResult> GetConcretteOrder(string id)
        {
            var concretteOrderResponseDTO = await _ordersRepository.GetConcretteOrder(id);
            return Ok(concretteOrderResponseDTO);
        }


        [HttpGet("")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public IActionResult GetListOfOrders()
        {
            var userEmail = User.Identity.Name;
            var orderResponseDTO = _ordersRepository.GetListOfOrders(userEmail);
            return Ok(orderResponseDTO);
        }


        [HttpPost("")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public async Task<IActionResult> CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO)
        {
            if (TimeChecker.ValidTime(DateTime.UtcNow, newOrderRequestDTO.DeliveryTime))
            {
                var userId = User.Identity.Name;
                await _ordersRepository.CreateNewOrder(newOrderRequestDTO, userId);
            }
            else
            {
                throw new Exception(ErrorCreator.CreateError("Недостаточно времени для доставки"));
            }
            return Ok();
        }


        [HttpPost("{id}/status")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public async Task<IActionResult> ConfirmOrderDelivery(string id)
        {
            await _ordersRepository.ConfirmOrderDelivery(id);
            return Ok();
        }

    }
}

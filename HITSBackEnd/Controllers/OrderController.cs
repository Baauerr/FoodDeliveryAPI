using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.DataValidation;
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

        public OrderController (IOrdersRepository ordersRepository, DeliveryTimeChecker timeChecker)
        {
            _ordersRepository = ordersRepository;
        }


        [HttpGet("{id}")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ConcretteOrderResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> GetConcretteOrder(Guid id)
        {
            var concretteOrderResponseDTO = await _ordersRepository.GetConcretteOrder(id);
            return Ok(concretteOrderResponseDTO);
        }


        [HttpGet("")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(List<OrderInListDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> GetListOfOrders()
        {
            var userEmail = User.Identity.Name;
            var orderResponseDTO = await _ordersRepository.GetListOfOrders(userEmail);
            return Ok(orderResponseDTO);
        }


        [HttpPost("")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> CreateNewOrder(NewOrderRequestDTO newOrderRequestDTO)
        {
            var userId = User.Identity.Name;
            await _ordersRepository.CreateNewOrder(newOrderRequestDTO, userId);
            return Ok();
        }


        [HttpPost("{id}/status")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> ConfirmOrderDelivery(Guid id)
        {
            var email = User.Identity.Name;
            await _ordersRepository.ConfirmOrderDelivery(id, email);
            return Ok();
        }

    }
}

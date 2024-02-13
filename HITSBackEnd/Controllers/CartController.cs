using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Dto.CartDTO;
using HITSBackEnd.Exceptions;
using HITSBackEnd.Services.UserCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly IUserCartRepository _userCartRepository;

        public CartController(IUserCartRepository userCartRepository)
        {
            _userCartRepository = userCartRepository;
        }

        [HttpPost("dish/{dishId}")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> AddDishToCart(Guid dishId)
        {
            var email = User.Identity.Name;
            await _userCartRepository.AddDishToCart(dishId, email);
            return Ok();
        }


        [HttpGet("")]
        [Authorize]
        [ProducesResponseType(typeof(GetCartDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> GetUserCart()
        {
            var email = User.Identity.Name;
            var UserCartDTO = await _userCartRepository.GetUserCart(email);
            return Ok(UserCartDTO);
        }


        [HttpDelete("dish/{dishId}")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> DeleteDishFromCart(Guid dishId, [FromQuery] bool increase)
        {
            var email = User.Identity.Name;
            await _userCartRepository.RemoveDishFromCart(email, dishId, increase);
            return Ok();
        }
    }
}

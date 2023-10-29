using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Services.Dishes.DishesRepository;
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
        public IActionResult AddDishToCart(string dishId)
        {
            var email = User.Identity.Name;
            _userCartRepository.AddDishToCart(dishId, email);
            return Ok();
        }
        [HttpGet("")]
        [Authorize]
        public IActionResult GetUserCart()
        {
            var email = User.Identity.Name;
            var UserCartDTO =  _userCartRepository.GetUserCart(email);
            return Ok(UserCartDTO);
        }
        [HttpDelete("dish/{dishId}")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public IActionResult DeleteDishFromCart(string dishId, [FromQuery] bool increase)
        {
            var email = User.Identity.Name;
            _userCartRepository.RemoveDishFromCart(email, dishId, increase);
            return NoContent();
        }
    }
}

using HITSBackEnd.Services.Dishes.DishesRepository;
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
    }
}

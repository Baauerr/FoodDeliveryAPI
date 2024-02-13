using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Exceptions;
using HITSBackEnd.Services.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HITSBackEnd.Controllers
{
    [Route("api/account")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(RegistrationLoginResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var RegistrationLoginResponceDTO = await _userService.Login(model);
            return Ok(RegistrationLoginResponceDTO);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegistrationLoginResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            var RegistrationLoginResponceDTO = await _userService.Register(model);
            return Ok(RegistrationLoginResponceDTO);
        }

        [HttpGet("profile")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> GetProfile()
        {
            var ProfileResponseDTO = await _userService.Profile(User.Identity.Name);
            return Ok(ProfileResponseDTO);
        }

        [HttpPost("logout")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> LogOut()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string email = User.Identity.Name;
            await _userService.LogOut(token, email);
            return Ok();
        }

        [HttpPut("profile")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> editUserProfile([FromBody] EditUserInfoRequestDTO model)
        {
            string email = User.Identity.Name;
            await _userService.EditUserInfo(model, email);
            return Ok();
        }

    }
}

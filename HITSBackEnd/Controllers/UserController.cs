using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Services.UserRepository;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HITSBackEnd.Controllers
{
    [Route("api/account")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var RegistrationLoginResponceDTO = await _userRepository.Login(model);
            if (string.IsNullOrEmpty(RegistrationLoginResponceDTO.Token))
            {
                throw new Exception(ErrorCreator.CreateError("Неверный формат телефона"));
            }
            return Ok(RegistrationLoginResponceDTO);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            var RegistrationLoginResponceDTO = await _userRepository.Register(model);
            return Ok(RegistrationLoginResponceDTO);
        }

        [HttpGet("profile")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public async Task<IActionResult> GetProfile()
        {
            var ProfileResponseDTO = await _userRepository.Profile(User.Identity.Name);
            return Ok(ProfileResponseDTO);
        }

        [HttpPost("logout")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public async Task<IActionResult> LogOut()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            string email = User.Identity.Name;
            await _userRepository.LogOut(token, email);
            return Ok();
        }

        [HttpPut("profile")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public async Task<IActionResult> editUserProfile([FromBody] EditUserInfoRequestDTO model)
        {
            string email = User.Identity.Name;
            await _userRepository.EditUserInfo(model, email);
            return Ok();
        }

    }
}

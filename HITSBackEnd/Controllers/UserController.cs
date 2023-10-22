using HITSBackEnd.Dto;
using HITSBackEnd.Services.IRepository;
using HITSBackEnd.Swagger;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            var RegistrationLoginResponceDTO = await _userRepository.Register(model);
            return Ok(RegistrationLoginResponceDTO);
        }
    }
}

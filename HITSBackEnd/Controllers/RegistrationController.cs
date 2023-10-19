using HITSBackEnd.baseClasses;
using HITSBackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class RegistrationController : ControllerBase
    {
        private readonly Registration _userRegistration;

        public RegistrationController(Registration userRegistration)
        {
            _userRegistration = userRegistration;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User request)
        {
        _userRegistration.RegistrateUser();

        return Ok("Registration successful");

        }
    }
}

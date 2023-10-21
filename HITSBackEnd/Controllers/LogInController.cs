using HITSBackEnd.baseClasses;
using HITSBackEnd.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    public class LogInController : Controller
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
            [HttpPost("login")]
            public IActionResult Register([FromBody] Users request)
            {
               

                return Ok("LogIn successful");

            }
        }
    }
}

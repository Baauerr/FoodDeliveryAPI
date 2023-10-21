using HITSBackEnd.baseClasses;
using HITSBackEnd.Services.Account;
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
        public IActionResult Register([FromBody] Users request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Invalid request data");
                }
                _userRegistration.RegistrateUser(
                    request.FullName,
                    request.Password,
                    request.Email,
                    request.Address,
                    request.BirthDate,
                    request.Gender,
                    request.PhoneNumber
                );
                string token = TokenGenerator.GenerateToken(request.Email, request.FullName);

                var result = new { token = token };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

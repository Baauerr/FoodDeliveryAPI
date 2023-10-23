using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class Test : Controller
    {
        [HttpGet("GetAuth")]
        public string getAuthorize()
        {
            return "bruh";
        }
    }
}

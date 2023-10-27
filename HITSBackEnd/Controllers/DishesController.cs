using HITSBackEnd.Dto.UserDTO;
using HITSBackEnd.Services.Account.IRepository;
using HITSBackEnd.Services.Dishes.DishesRepository;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [Route("api/order")]
    public class DishesController : Controller
    {
        private readonly IDishesRepository _dishesRepository;

        public DishesController(IDishesRepository dishesRepository)
        {
            _dishesRepository = dishesRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetConcretteDish( string id)
        {
           var dishResponseDTO = _dishesRepository.GetConcretteDish(id);
            
            return Ok(dishResponseDTO);
        }
    }
}

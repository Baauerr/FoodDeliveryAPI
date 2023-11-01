using HITSBackEnd.Services.Dishes.DishesRepository;
using Microsoft.AspNetCore.Mvc;

namespace HITSBackEnd.Controllers
{
    [Route("api/dish")]
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

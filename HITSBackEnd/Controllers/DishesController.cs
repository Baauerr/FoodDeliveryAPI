using HITSBackEnd.Services.Dishes.DishesRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetConcretteDish(string id)
        {
            var dishResponseDTO = _dishesRepository.GetConcretteDish(id);

            return Ok(dishResponseDTO);
        }
        [HttpGet]
        public IActionResult GetDishes([FromQuery] List<Category> categories, [FromQuery] bool? isVegetarian, [FromQuery] SortingTypes sorting, [FromQuery] int page)
        {
            return Ok(_dishesRepository.GetDishesPage(categories, isVegetarian, sorting, page));
        }
    }
}

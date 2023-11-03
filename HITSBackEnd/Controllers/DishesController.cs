using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Services.Dishes.DishesRepository;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetConcretteDish(string id)
        {
            var concretteDish = await _dishesRepository.GetConcretteDish(id);

            return Ok(concretteDish);
        }


        [HttpGet("{id}/rating/check")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public async Task<IActionResult> RatingCheck(string id)
        {
            var userEmail = User.Identity.Name;

            var opportunityToRate = await _dishesRepository.RatingCheck(id, userEmail);

            return Ok(opportunityToRate);
        }


        [HttpGet]
        public IActionResult GetDishes([FromQuery] List<Category> categories, [FromQuery] bool? isVegetarian, [FromQuery] SortingTypes sorting, [FromQuery] int page)
        {
            var dishesPage = _dishesRepository.GetDishesPage(categories, isVegetarian, sorting, page);

            return Ok(dishesPage);
        }


        [HttpPost("{id}/rating")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        public async Task<IActionResult> SetRating([FromQuery] double ratingScore, string id)
        {
            if (ratingScore > 10)
            {
                throw new Exception(ErrorCreator.CreateError("Оценка блюда не может быть больше 10"));
            }

            var userEmail = User.Identity.Name;

            await _dishesRepository.SetRating(id, userEmail, ratingScore);

            return Ok();
        }
    }
}

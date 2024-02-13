using HITSBackEnd.Controllers.AttributeUsage;
using HITSBackEnd.Dto.DishDTO;
using HITSBackEnd.Models.DishesModels;
using HITSBackEnd.Services.Dishes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HITSBackEnd.Exceptions;

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
        [ProducesResponseType(typeof(DishTable), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        public async Task<IActionResult> GetConcretteDish(Guid id)
        {
            var concretteDish = await _dishesRepository.GetConcretteDish(id);

            return Ok(concretteDish);
        }


        [HttpGet("{id}/rating/check")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> RatingCheck(Guid id)
        {
            var userEmail = User.Identity.Name;

            var opportunityToRate = await _dishesRepository.RatingCheck(id, userEmail);

            return Ok(opportunityToRate);
        }


        [HttpGet]
        [ProducesResponseType(typeof(DishPageResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public IActionResult GetDishes([FromQuery] List<Category> categories, [FromQuery] bool? isVegetarian, [FromQuery] SortingTypes sorting, [FromQuery] int page)
        {
            var dishesPage = _dishesRepository.GetDishesPage(categories, isVegetarian, sorting, page);

            return Ok(dishesPage);
        }


        [HttpPost("{id}/rating")]
        [Authorize]
        [ServiceFilter(typeof(TokenBlacklistFilterAttribute))]
        [ProducesResponseType(typeof(ErrorResponseModel), 401)]
        [ProducesResponseType(typeof(ErrorResponseModel), 403)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> SetRating([Range(0, 10)]double ratingScore, Guid id)
        {
            var userEmail = User.Identity.Name;

            await _dishesRepository.SetRating(id, userEmail, ratingScore);

            return Ok();
        }
    }
}

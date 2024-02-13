using HITSBackEnd.DataBaseContext;
using HITSBackEnd.Dto.DishDTO;
using HITSBackEnd.Exceptions;
using HITSBackEnd.Models.DishesModels;
using HITSBackEnd.Services.Dishes;
using Microsoft.EntityFrameworkCore;

namespace HITSBackEnd.Services.Dish
{
    public class DishesRepository : IDishesRepository
    {

        private readonly AppDbContext _db;

        public DishesRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> RatingCheck(Guid dishId, string userEmail)
        {
            if (userEmail == null)
            {
                throw new BadRequestException("Токен не валиден");
            }

            var checkDishInMenu = await _db.Dishes.AnyAsync(dish => dish.Id == dishId);

            if (!checkDishInMenu) 
            {
                throw new NotFoundException("Такого блюда нет в меню");
            }

            var ordersId = await _db.OrdersDishes
                .Where(d => d.DishId == dishId)
                .Select(d => d.OrderId)
                .ToListAsync();

            var userOrderChecker = await _db.Orders.AnyAsync(order => order.UserEmail == userEmail && ordersId.Contains(order.Id));

            return userOrderChecker;
        }

        public async Task<DishTable> GetConcretteDish(Guid id)
        {
            var dish = await _db.Dishes.FirstOrDefaultAsync(u => u.Id == id);

            if (dish == null)
            {
                throw new NotFoundException("Такого блюда нет в меню");
            }

            DishResponseDTO response = new DishResponseDTO
            {
                Id = dish.Id,
                Name = dish.Name,
                Category = dish.Category,
                Price = dish.Price,
                Description = dish.Description,
                IsVegeterian = dish.IsVegetarian,
                Photo = dish.Photo
            };

            return dish;
        }
        public DishPageResponseDTO GetDishesPage(List<Category> categories, bool? isVegetarian, SortingTypes sorting, int page)
        {
            var allDishes = _db.Dishes.AsQueryable();

            List<DishResponseDTO> dishList = new List<DishResponseDTO>();

            if (isVegetarian != null)
            {
                allDishes = allDishes.Where(d => d.IsVegetarian == isVegetarian);
            }

            if (categories != null && categories.Any())
            {
                allDishes = allDishes.Where(d => categories.Contains(d.Category));
            }

            if (page <= 0)
            {
                page = 1;
            }

            allDishes = SortDishes(allDishes, sorting);

            const int sizeOfPage = 5;
            var countOfPages = (int)Math.Ceiling((double)allDishes.Count() / sizeOfPage);
            if (page <= countOfPages)
            {
                var lowerBound = page == 1 ? 0 : (page - 1) * sizeOfPage;
                if (page < countOfPages)
                {
                    allDishes = allDishes.Skip(lowerBound).Take(sizeOfPage);
                }
                else
                {
                    allDishes = allDishes.Skip(lowerBound).Take(allDishes.Count() - lowerBound);
                }
            }
            else
            {
                throw new BadRequestException("Такой страницы нет");
            }

            var paginationDto = new PaginationDto
            {
                Current = page,
                Count = countOfPages,
                Size = sizeOfPage
            };

            var pageDto = new DishPageResponseDTO
            {
                Dishes = allDishes,
                Pagination = paginationDto
            };
            return pageDto;
        }

        private static IQueryable<DishTable> SortDishes(IQueryable<DishTable> allDishes, SortingTypes sorting)
        {
            switch (sorting)
            {
                case SortingTypes.NameAsc:
                    allDishes = allDishes.OrderBy(d => d.Name);
                    break;

                case SortingTypes.NameDesc:
                    allDishes = allDishes.OrderByDescending(d => d.Name);
                    break;

                case SortingTypes.PriceAsc:
                    allDishes = allDishes.OrderBy(d => d.Price);
                    break;

                case SortingTypes.PriceDesc:
                    allDishes = allDishes.OrderByDescending(d => d.Price);
                    break;

                case SortingTypes.RatingAsc:
                    allDishes = allDishes.OrderBy(d => d.Name);
                    break;

                case SortingTypes.RatingDesc:
                    allDishes = allDishes.OrderByDescending(d => d.Name);
                    break;
            }
            return allDishes;
        }

        public async Task SetRating(Guid dishId, string userEmail, double rate)
        {
            if (userEmail == null)
            {
                throw new BadRequestException("Токен не валиден");
            }

            if (!(await RatingCheck(dishId, userEmail)))
            {
                throw new ForbiddenException("Блюдо ещё не было заказано пользователем");
            }

            var dishToRate = await _db.DishesRating.FirstOrDefaultAsync(dish => dish.DishId == dishId && dish.UserEmail == userEmail);
            if (dishToRate == null)
            {
                var newRate = new RatingTable()
                {
                    DishId = dishId,
                    UserEmail = userEmail,
                    Value = rate
                };
                await _db.DishesRating.AddAsync(newRate);
            }
            else
            {
                dishToRate.Value = rate;
            }
            await _db.SaveChangesAsync();
            await UpdateDishRate(dishId);

        }
        private async Task UpdateDishRate(Guid dishId)
        {
            var dish = await _db.Dishes.FirstOrDefaultAsync(dish => dish.Id == dishId);
            var averageValue = _db.DishesRating
                .Where(r => r.DishId == dishId)
                .Average(r => r.Value);
            dish.rating = averageValue;
            await _db.SaveChangesAsync();
        }
    }
}

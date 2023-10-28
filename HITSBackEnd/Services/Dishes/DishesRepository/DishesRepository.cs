using HITSBackEnd.DataBase;
using HITSBackEnd.Dto.DishDTO;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using static Azure.Core.HttpHeader;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    public class DishesRepository : IDishesRepository
    {

        private readonly AppDbContext _db;

        public DishesRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool RatingCheck()
        {
            throw new NotImplementedException();
        }

        public DishResponseDTO GetConcretteDish(string id)
        {
            Guid idGuid;
            if (!Guid.TryParse(id, out idGuid))
            {
                throw new Exception(ErrorCreator.CreateError("Такого блюда нет в меню"));
            }

            var dish = _db.Dishes.FirstOrDefault(u => u.Id == idGuid);

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

            return response;
        }
        public DishPageResponseDTO GetDishesPage(List<Category> categories, bool? isVegetarian, SortingTypes sorting, int page)
        {
            var allDishes = _db.Dishes.AsQueryable();

            List <DishResponseDTO> dishList = new List <DishResponseDTO>();

            if (isVegetarian != null)
            {
                allDishes = allDishes.Where(d => d.IsVegetarian == isVegetarian);
            }

            if (categories != null && categories.Any())
            {
                allDishes = allDishes.Where(d => categories.Contains(d.Category));
            }

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

            const int sizeOfPage = 5;
            int countOfPages = (int)Math.Ceiling((double)allDishes.Count() / sizeOfPage);
            int lowerBound = 0;
            int upperBound = 0;
            if (page <= countOfPages)
            {
                lowerBound = (page == 1) ? 0 : ((page - 1) * sizeOfPage);
                if (page < countOfPages) {
                    upperBound = lowerBound + sizeOfPage;
                    allDishes = allDishes.Skip(lowerBound).Take(sizeOfPage);
                }
                else
                {
                    allDishes = allDishes.Skip(lowerBound).Take(allDishes.Count() - lowerBound);
                }  
            }
            else
            {
                throw new Exception(ErrorCreator.CreateError("Такой страницы нет"));
            }

            PaginationDTO paginationDTO = new PaginationDTO();

            paginationDTO.Current = page;
            paginationDTO.Count = countOfPages;
            paginationDTO.Size = sizeOfPage;

            DishPageResponseDTO pageDTO = new DishPageResponseDTO();
            pageDTO.Dishes = allDishes;
            pageDTO.Pagination = paginationDTO;
            return pageDTO;
        }
    }
}

namespace HITSBackEnd.Dto.DishDTO
{
    public class DishPageResponseDTO
    {
        public IQueryable Dishes { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}

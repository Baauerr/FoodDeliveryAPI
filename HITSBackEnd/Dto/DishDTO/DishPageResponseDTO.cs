namespace HITSBackEnd.Dto.DishDTO
{
    public class DishPageResponseDTO
    {
        public IQueryable Dishes { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}

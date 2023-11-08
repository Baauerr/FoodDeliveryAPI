using Npgsql.PostgresTypes;

namespace HITSBackEnd.Dto.CartDTO
{
    public class UserCartDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public int Amount { get; set; }
        public string Image{  get; set; }
    }
}

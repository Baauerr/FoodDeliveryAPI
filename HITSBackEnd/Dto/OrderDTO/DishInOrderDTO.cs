namespace HITSBackEnd.Dto.OrderDTO
{
    public class DishInOrderDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public double TotalPrice { get; set; }
        public int Amount { get; set; }
        public string Image {  get; set; }
    }
}

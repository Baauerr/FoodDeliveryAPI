namespace HITSBackEnd.baseClasses
{
    public class Order
    {
        public DateTime DeliveryTime {  get; set; }
        public DateTime OrderTime { get; set; }
        public double Price { get; set; }
        public string AdressId {  get; set; }
    }
}

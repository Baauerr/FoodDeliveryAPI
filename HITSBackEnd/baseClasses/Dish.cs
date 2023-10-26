namespace HITSBackEnd.baseClasses
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsVegeterian { get; set; }
        public  string Image {  get; set; }
        
    }
}

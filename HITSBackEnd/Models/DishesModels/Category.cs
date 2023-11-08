using System.Text.Json.Serialization;

namespace HITSBackEnd.Models.DishesModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        WOK,
        Pizza,
        Soup,
        Dessert,
        Drink
    }
}

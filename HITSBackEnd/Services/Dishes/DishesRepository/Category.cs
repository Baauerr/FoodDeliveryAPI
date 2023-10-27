using System.Text.Json.Serialization;

namespace HITSBackEnd.Services.Dishes.DishesRepository
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

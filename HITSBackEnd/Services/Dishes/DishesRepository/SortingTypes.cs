using System.Text.Json.Serialization;

namespace HITSBackEnd.Services.Dishes.DishesRepository
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortingTypes
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
        RatingAsc,
        RatingDesc
    }
}

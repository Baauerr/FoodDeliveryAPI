using System.Text.Json.Serialization;

namespace HITSBackEnd.Models.DishesModels
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

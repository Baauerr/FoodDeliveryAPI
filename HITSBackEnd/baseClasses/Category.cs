using System.Text.Json.Serialization;

namespace HITSBackEnd.baseClasses
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        WOK, 
        Pizza,
        Soup, 
        Desert, 
        Drink
    }
}

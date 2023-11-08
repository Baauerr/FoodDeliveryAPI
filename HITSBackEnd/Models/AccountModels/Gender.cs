using System.Text.Json.Serialization;

namespace HITSBackEnd.Models.AccountModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        FEMALE,
        MALE
    }
}

using System.Text.Json.Serialization;

namespace HITSBackEnd.Services.Account.UserRepository
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        FEMALE,
        MALE
    }
}

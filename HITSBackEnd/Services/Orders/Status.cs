using System.Text.Json.Serialization;

namespace HITSBackEnd.Services.Orders
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        inProcess,
        Delivered
    }
}

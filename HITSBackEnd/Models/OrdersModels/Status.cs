using System.Text.Json.Serialization;

namespace HITSBackEnd.Models.OrdersModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        inProcess,
        Delivered
    }
}

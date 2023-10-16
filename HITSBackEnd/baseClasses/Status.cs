using System.Text.Json.Serialization;

namespace HITSBackEnd.baseClasses
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        inProcess,
        Delivered
    }
}

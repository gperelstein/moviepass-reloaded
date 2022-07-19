using System.Text.Json.Serialization;

namespace MPR.Shared.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatus
    {
        New = 0,
        Success = 1,
        Failed = 2
    }
}

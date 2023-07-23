using System.Text.Json.Serialization;

namespace firstapi.Models
{

    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Religion
    {

        Islam,
        Other,
    }
}
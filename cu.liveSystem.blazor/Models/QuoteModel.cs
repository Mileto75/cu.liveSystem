using System.Text.Json.Serialization;

namespace cu.liveSystem.blazor.Models
{
    public class QuoteModel
    {
        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}

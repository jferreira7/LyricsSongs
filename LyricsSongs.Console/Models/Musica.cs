using System.Text.Json.Serialization;

namespace LyricsSongs.Console.Models
{
    public class Musica
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        [JsonPropertyName("title")]
        public string? Titulo { get; set; }
        [JsonPropertyName("band")]
        public string? Banda { get; set; }
    }
}

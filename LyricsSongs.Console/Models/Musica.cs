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
        public string? Nome { get; set; }
        [JsonPropertyName("band")]
        public string? Banda { get; set; }
        public string? LetraOriginal { get; set; }
        public string? LetraTraduzida { get; set; }
    }
}

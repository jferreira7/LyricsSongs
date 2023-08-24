using LyricsSongs.Console.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LyricsSongs.Console.Services
{
    using System;

    public class ApiVagalumeService : IApiVagalumeService
    {
        private string _apiKey { get; set; } = string.Empty;
        public Dictionary<int, Musica> MusicasBuscadas { get; set; } = new();

        private Musica musicaSelecionada = new();

        public void SetApiKey(string token)
        {
            _apiKey = token;
        }

        public async Task<Dictionary<int, Musica>> SearchMusicas(string textoPesquisa)
        {
            MusicasBuscadas.Clear();
            using (HttpClient client = new())
            {
                try
                {
                    string url = $"https://api.vagalume.com.br/search.artmus?apikey={_apiKey}&q={textoPesquisa}&limit=5";
                    string retorno = await client.GetStringAsync(url);

                    JsonNode? retornoObjeto = JsonSerializer.Deserialize<JsonNode>(retorno);
                    JsonArray? docs = retornoObjeto?["response"]?["docs"]?.AsArray();

                    for (int i = 0, z = 0; i < docs?.Count; i++)
                    {
                        if (docs?[i]?["title"] == null) continue;

                        MusicasBuscadas.Add(++z, new Musica()
                        {
                            Id = docs?[i]?["id"]?.ToString(),
                            Nome = docs?[i]?["title"]?.ToString(),
                            Banda = docs?[i]?["band"]?.ToString(),
                            Url = docs?[i]?["url"]?.ToString(),
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return MusicasBuscadas;
        }

        public async Task<Musica> getMusicaSelecionada(int musicaEscolhida)
        {
            if (MusicasBuscadas.ContainsKey(musicaEscolhida))
            {
                using (HttpClient client = new())
                {
                    try
                    {
                        Guid guid = Guid.NewGuid();
                        string url = $"https://api.vagalume.com.br/search.php?musid={MusicasBuscadas[musicaEscolhida].Id}&apikey={_apiKey}&hash={guid.ToString()}";
                        var retorno = await client.GetStringAsync(url);

                        JsonNode? retornoObjeto = JsonSerializer.Deserialize<JsonNode>(retorno);
                        JsonNode? musicaInfo = retornoObjeto?["mus"];
                        JsonNode? bandaInfo = retornoObjeto?["art"];

                        musicaSelecionada = new()
                        {
                            Id = musicaInfo?[0]?["id"]?.ToString(),
                            Url = musicaInfo?[0]?["url"]?.ToString(),
                            Nome = musicaInfo?[0]?["name"]?.ToString(),
                            Banda = bandaInfo?["name"]?.ToString(),
                            LetraOriginal = musicaInfo?[0]?["text"]?.ToString(),
                            LetraTraduzida = musicaInfo?[0]?["translate"]?[0]?["text"]?.ToString()
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else
            {
                Console.WriteLine("Opção selecionada inválida!!");
            }

            return musicaSelecionada;
        }
    }
}
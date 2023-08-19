namespace LyricsSongs.Console.API
{
    using LyricsSongs.Console.Models;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Nodes;

    public static class ApiVagalume
    {
        private static readonly string API_KEY = "d8f3e5fabca6e3e0ab123723bcd3e918";

        private static Dictionary<int, Musica> musicasBuscadas = new();
        private static Musica musicaSelecionada = new();

        public static async Task<Dictionary<int, Musica>> SearchMusicas(string textoPesquisa)
        {
            musicasBuscadas.Clear();
            using (HttpClient client = new())
            {
                try
                {
                    string url = $"https://api.vagalume.com.br/search.artmus?apikey={API_KEY}&q={textoPesquisa}&limit=5";
                    string retorno = await client.GetStringAsync(url);

                    JsonNode? retornoObjeto = JsonSerializer.Deserialize<JsonNode>(retorno);
                    JsonArray? docs = retornoObjeto?["response"]?["docs"]?.AsArray();

                    for (int i = 0, z = 0; i < docs?.Count; i++)
                    {
                        if (docs?[i]?["title"] == null) continue;

                        musicasBuscadas.Add(++z, new Musica()
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

            return musicasBuscadas;
        }

        public static async Task<Musica> getMusicaSelecionada(int musicaEscolhida)
        {
            if (musicasBuscadas.ContainsKey(musicaEscolhida))
            {
                using (HttpClient client = new())
                {
                    Guid guid = Guid.NewGuid();
                    string url = $"https://api.vagalume.com.br/search.php?musid={musicasBuscadas[musicaEscolhida].Id}&apikey={API_KEY}&hash={guid.ToString()}";
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
            }
            else
            {
                Console.WriteLine("Opção selecionada inválida!!");
            }

            return musicaSelecionada;
        }
    }
}
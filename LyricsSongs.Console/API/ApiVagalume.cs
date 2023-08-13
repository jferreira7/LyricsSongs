namespace LyricsSongs.Console.API
{
    using LyricsSongs.Console.Models;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    public class ApiVagalume
    {
        private readonly string API_KEY = "d8f3e5fabca6e3e0ab123723bcd3e918";

        private Dictionary<int, Musica> musicasBuscadas;
        private Musica musicaSelecionada;

        public ApiVagalume()
        {
            this.musicasBuscadas = new();
            this.musicaSelecionada = new();
        }

        public async Task<Dictionary<int, Musica>> SearchMusicas(string textoPesquisa)
        {
            using (HttpClient client = new())
            {
                try
                {
                    //string url = $"https://api.vagalume.com.br/search.excerpt?q={textoPesquisa}&limit=10";
                    string url = $"https://api.vagalume.com.br/search.artmus?apikey={this.API_KEY}&q={textoPesquisa}&limit=5";
                    string retorno = await client.GetStringAsync(url);

                    var retornoObjeto = JsonDocument.Parse(retorno);
                    var docs = retornoObjeto.RootElement.GetProperty("response").GetProperty("docs");
                    List<Musica> musicas = JsonSerializer.Deserialize<List<Musica>>(docs.ToString())!;

                    int z = 0;
                    for (int i = 0; i < musicas.Count; i++)
                    {
                        if (musicas[i].Nome != null)
                        {
                            this.musicasBuscadas.Add(++z, musicas[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);

                }
            }

            return this.musicasBuscadas;
        }



        public async Task<Musica> getMusicaSelecionada(int musicaEscolhida)
        {
            if (this.musicasBuscadas.ContainsKey(musicaEscolhida))
            {
                using (HttpClient client = new())
                {
                    string url = $"https://api.vagalume.com.br/search.php?musid={this.musicasBuscadas[musicaEscolhida].Id}&apikey={this.API_KEY}";
                    var retorno = await client.GetStringAsync(url);

                    var retornoObjeto = JsonDocument.Parse(retorno);
                    var musicaInfo = retornoObjeto.RootElement.GetProperty("mus");
                    var bandaInfo = retornoObjeto.RootElement.GetProperty("art");

                    this.musicaSelecionada = new()
                    {
                        Id = musicaInfo[0].GetProperty("id").ToString(),
                        Url = musicaInfo[0].GetProperty("url").ToString(),
                        Nome = musicaInfo[0].GetProperty("name").ToString(),
                        Banda = bandaInfo.GetProperty("name").ToString(),
                        LetraOriginal = musicaInfo[0].GetProperty("text").ToString(),
                        LetraTraduzida = musicaInfo[0].TryGetProperty("translate", out var translateElement) && translateElement.TryGetProperty("text", out var textElement) ? textElement.ToString() : null
                    };
                }
            }
            else
            {
                Console.WriteLine("Opção selecionada inválida!");
            }

            return this.musicaSelecionada;
        }
    }
}

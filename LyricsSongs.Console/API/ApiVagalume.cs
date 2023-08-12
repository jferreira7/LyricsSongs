namespace LyricsSongs.Console.API
{
    using LyricsSongs.Console.Models;
    using System;
    using System.Collections.Generic;
    using System.Text.Json;

    public class ApiVagalume
    {
        private Dictionary<int, Musica> musicasBuscadas = new();
        private readonly string API_KEY = "d8f3e5fabca6e3e0ab123723bcd3e918";
        public async Task SearchMusicas(string textoPesquisa)
        {
            using (HttpClient client = new())
            {
                try
                {
                    string retorno = await client.GetStringAsync($"https://api.vagalume.com.br/search.excerpt?q={textoPesquisa}&limit=5");

                    var retornoObjeto = JsonDocument.Parse(retorno);
                    var docs = retornoObjeto.RootElement.GetProperty("response").GetProperty("docs");
                    List<Musica> musicas = JsonSerializer.Deserialize<List<Musica>>(docs.ToString())!;

                    for (int i = 0; i < musicas.Count; i++)
                        this.musicasBuscadas.Add(i + 1, musicas[i]);

                    mostrarMusicasBuscadas();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            }
        }

        public void mostrarMusicasBuscadas()
        {
            Console.WriteLine("Músicas encontradas: ");
            foreach (var item in this.musicasBuscadas)
                Console.WriteLine($"{item.Key} - {item.Value.Titulo} - {item.Value.Banda}");

            Console.Write("\nSelecione a música correta:");
            getMusicaEscolhida(Console.Read());
        }

        public void getMusicaEscolhida(int musicaEscolhida)
        {
            if (this.musicasBuscadas.ContainsKey(musicaEscolhida))
            {
                using (HttpClient client = new())
                {
                    string url = $"https://api.vagalume.com.br/search.php?musid={this.musicasBuscadas[musicaEscolhida].Id}&apikey={this.API_KEY}";
                    client.GetStringAsync(url);
                }
            }
            else
            {
                Console.WriteLine("Opção selecionada inválida!");
            }
        }
    }
}

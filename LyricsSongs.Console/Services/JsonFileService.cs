using LyricsSongs.Console.Models;
using LyricsSongs.Console.Services;
using System.Text.Json;

namespace LyricsSongs.Console.Service
{
    public class JsonFileService : IJsonFileService
    {
        public List<Musica> musicasFavoritasSalvas { get; set; }
        private bool jaPegouMusicasArquivoJson;

        public JsonFileService()
        {
            this.jaPegouMusicasArquivoJson = false;
            this.musicasFavoritasSalvas = new();
        }

        public async void Adicionar(Musica musica)
        {
            this.musicasFavoritasSalvas.Add(musica);

            if (!this.jaPegouMusicasArquivoJson)
                await this.GetMusicasDoArquivoJson();

            this.SalvarNoArquivoJson();
        }

        public void SalvarNoArquivoJson()
        {
            if (this.musicasFavoritasSalvas.Count == 0) return;

            string musicasFavoritas = JsonSerializer.Serialize<List<Musica>>(this.musicasFavoritasSalvas);
            File.WriteAllText("musicas_favoritas.json", musicasFavoritas);
        }

        public async Task GetMusicasDoArquivoJson()
        {
            string pathJsonFile = "musicas_favoritas.json";

            if (!File.Exists(pathJsonFile)) return;
            string jsonContentText = await File.ReadAllTextAsync(pathJsonFile);

            if (jsonContentText == "") return;
            List<Musica> jsonContentObject = JsonSerializer.Deserialize<List<Musica>>(jsonContentText)!;
            this.musicasFavoritasSalvas.InsertRange(0, jsonContentObject);
            this.jaPegouMusicasArquivoJson = true;
        }
    }
}
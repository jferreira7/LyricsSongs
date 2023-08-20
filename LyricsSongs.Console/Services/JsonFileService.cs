using LyricsSongs.Console.Models;
using LyricsSongs.Console.Services;
using System.Text.Json;

namespace LyricsSongs.Console.Service
{
    public class JsonFileService : IJsonFileService
    {
        public List<Musica> musicasFavoritasSalvas { get; set; }
        //private bool jaPegouMusicasArquivoJson;

        public JsonFileService()
        {
            //this.jaPegouMusicasArquivoJson = false;
            this.musicasFavoritasSalvas = new();
        }

        public async Task Adicionar(Musica musica)
        {
            musica.IsMusicaFavorita = true;
            this.musicasFavoritasSalvas.Add(musica);

            //if (!this.jaPegouMusicasArquivoJson)
            //    await this.GetMusicasDoArquivoJson();

            await this.SalvarNoArquivoJson();
        }

        public async Task Remover(Musica musica)
        {
            this.musicasFavoritasSalvas.RemoveAll(musicaSalva => musicaSalva.Id == musica.Id);

            await this.SalvarNoArquivoJson();
        }

        public async Task SalvarNoArquivoJson()
        {
            if (this.musicasFavoritasSalvas.Count == 0) return;

            string musicasFavoritas = JsonSerializer.Serialize<List<Musica>>(this.musicasFavoritasSalvas);
            await File.WriteAllTextAsync("musicas_favoritas.json", musicasFavoritas);
        }

        public async Task GetMusicasDoArquivoJson()
        {
            string pathJsonFile = "musicas_favoritas.json";

            if (!File.Exists(pathJsonFile)) return;
            string jsonContentText = await File.ReadAllTextAsync(pathJsonFile);

            if (jsonContentText == "") return;
            this.musicasFavoritasSalvas = JsonSerializer.Deserialize<List<Musica>>(jsonContentText)!;
            //List<Musica> jsonContentObject = JsonSerializer.Deserialize<List<Musica>>(jsonContentText)!;

            //this.musicasFavoritasSalvas.InsertRange(0, jsonContentObject);
            //this.jaPegouMusicasArquivoJson = true;
        }
    }
}
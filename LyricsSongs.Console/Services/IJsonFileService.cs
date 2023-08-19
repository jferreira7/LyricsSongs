using LyricsSongs.Console.Models;

namespace LyricsSongs.Console.Services
{
    public interface IJsonFileService
    {
        public List<Musica> musicasFavoritasSalvas { get; set; }

        public void Adicionar(Musica musica);

        public void SalvarNoArquivoJson();

        public Task GetMusicasDoArquivoJson() { return Task.CompletedTask; }
    }
}
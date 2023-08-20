using LyricsSongs.Console.Models;

namespace LyricsSongs.Console.Services
{
    public interface IJsonFileService
    {
        public List<Musica> musicasFavoritasSalvas { get; set; }

        public Task Adicionar(Musica musica);

        public Task Remover(Musica musica);

        public Task SalvarNoArquivoJson();

        public Task GetMusicasDoArquivoJson() { return Task.CompletedTask; }
    }
}
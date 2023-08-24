using LyricsSongs.Console.Models;

namespace LyricsSongs.Console.Services
{
    public interface IApiVagalumeService
    {
        Dictionary<int, Musica> MusicasBuscadas { get; set; }

        public void SetApiKey(string token);

        public Task<Dictionary<int, Musica>> SearchMusicas(string textoPesquisa);

        public Task<Musica> getMusicaSelecionada(int musicaEscolhida);
    }
}
namespace LyricsSongs.Console.Services
{
    public interface ITokenService
    {
        public void CheckIfTokenExists();

        public void StorageToken(string token);

        public void DeleteStoredToken();

        public string? GetStoredToken();
    }
}
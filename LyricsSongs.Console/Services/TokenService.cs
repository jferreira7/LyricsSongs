namespace LyricsSongs.Console.Services
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class TokenService : ITokenService
    {
        public void CheckIfTokenExists()
        {
            string? token = GetStoredToken();

            if (token != null)
            {
                (ServiceProviderFactory.GetService<IApiVagalumeService>()).SetApiKey(token);
            }
            else
            {
                do
                {
                    Console.Write("Digite um token válido (https://api.vagalume.com.br/): ");
                    token = Console.ReadLine();

                    if (token != null && token.Trim() != "")
                    {
                        StorageToken(token);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Token inválido");
                    }
                } while (true);
            }
        }

        public void StorageToken(string token)
        {
            try
            {
                byte[] toEncrypt = UnicodeEncoding.ASCII.GetBytes(token);

                FileStream fStream = new FileStream("Data.dat", FileMode.OpenOrCreate);

                byte[] entropy = UnicodeEncoding.ASCII.GetBytes("4b17b4a3-2994-401c-93b2-d77a441eb35f");

                EncryptDataToStream(toEncrypt, entropy, DataProtectionScope.CurrentUser, fStream);

                fStream.Close();

                Console.WriteLine("Token cadastrado!");
                Thread.Sleep(2000);
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
        }

        public void DeleteStoredToken()
        {
            try
            {
                File.Delete("Data.dat");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
        }

        public string? GetStoredToken()
        {
            try
            {
                FileStream fStream = new FileStream("Data.dat", FileMode.Open);

                byte[] entropy = UnicodeEncoding.ASCII.GetBytes("4b17b4a3-2994-401c-93b2-d77a441eb35f");

                byte[] decryptData = DecryptDataFromStream(entropy, DataProtectionScope.CurrentUser, fStream, 262);

                fStream.Close();

                return UnicodeEncoding.ASCII.GetString(decryptData);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public byte[] CreateRandomEntropy()
        {
            byte[] entropy = new byte[16];

            new RNGCryptoServiceProvider().GetBytes(entropy);

            return entropy;
        }

        public int EncryptDataToStream(byte[] Buffer, byte[] Entropy, DataProtectionScope Scope, Stream S)
        {
            if (Buffer == null)
                throw new ArgumentNullException(nameof(Buffer));
            if (Buffer.Length <= 0)
                throw new ArgumentException("The buffer length was 0.", nameof(Buffer));
            if (Entropy == null)
                throw new ArgumentNullException(nameof(Entropy));
            if (Entropy.Length <= 0)
                throw new ArgumentException("The entropy length was 0.", nameof(Entropy));
            if (S == null)
                throw new ArgumentNullException(nameof(S));

            int length = 0;

            byte[] encryptedData = ProtectedData.Protect(Buffer, Entropy, Scope);

            if (S.CanWrite && encryptedData != null)
            {
                S.Write(encryptedData, 0, encryptedData.Length);
                length = encryptedData.Length;
            }

            return length;
        }

        public byte[] DecryptDataFromStream(byte[] Entropy, DataProtectionScope Scope, Stream S, int Length)
        {
            if (S == null)
                throw new ArgumentNullException(nameof(S));
            if (Length <= 0)
                throw new ArgumentException("The given length was 0.", nameof(Length));
            if (Entropy == null)
                throw new ArgumentNullException(nameof(Entropy));
            if (Entropy.Length <= 0)
                throw new ArgumentException("The entropy length was 0.", nameof(Entropy));

            byte[] inBuffer = new byte[Length];
            byte[] outBuffer;

            if (S.CanRead)
            {
                S.Read(inBuffer, 0, Length);

                outBuffer = ProtectedData.Unprotect(inBuffer, Entropy, Scope);
            }
            else
            {
                throw new IOException("Could not read the stream.");
            }

            return outBuffer;
        }
    }
}
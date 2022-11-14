using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class PasswordHashHelper
    {
        public static string HashPassword(string password, string salt)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes($"{password}{salt}");
            var hashPassword = Convert.ToBase64String(hash.ComputeHash(passwordBytes));
            return hashPassword;
        }

        public static string GenerateSalt()
        {
            int max_length = 32;
            var randomNumber = new byte[max_length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
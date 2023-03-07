using SuggestionPanel.Domain.Models;
using System.Security.Cryptography;

namespace SuggestionPanel.Application.Services.Authentication
{
    /// <summary>
    /// Helper class to generate Password Hash and its Salt
    /// </summary>
    public static class AuthHelper
    {
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        /// <summary>
        /// This method generates array of bytes fulled with random numbers
        /// </summary>
        /// <param name="size">Size of byte array</param>
        /// <returns>Salt - byte array</returns>
        private static byte[] GenerateSalt(int size)
        {
            var salt = new byte[size];
            rng.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Generates PasswordHash with Hash and Salt combined
        /// </summary>
        /// <param name="password">Unhashed password</param>
        /// <param name="salt">Generated Salt</param>
        /// <returns>String of combined hash and salt in base64</returns>
        public static string GenerateHash(string password, string salt)
        {
            var salt1 = Convert.FromBase64String(salt);

            using var hashGenerator = new Rfc2898DeriveBytes(password, salt1);
            hashGenerator.IterationCount = 10101;
            var bytes = hashGenerator.GetBytes(24);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts ValueStreamResponsibility Password into Hash and Generates the Salt
        /// </summary>
        /// <param name="vsr">ValueStreamResponsibility class with unshashed password and without Salt</param>
        public static void ProvideSaltAndHash(this ValueStreamResponsibility vsr)
        {
            var salt = GenerateSalt(24);
            vsr.Salt = Convert.ToBase64String(salt);
            vsr.PasswordHash = GenerateHash(vsr.PasswordHash, vsr.Salt);
        }
    }
}

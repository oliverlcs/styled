using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Styled.Api.Services
{
    public class PasswordHasher
    {
        public static string Hash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 210_000,
                numBytesRequested: 256 / 8
            ));

            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        public static bool Verify(string hashedPasswordWithSalt, string passwordToCheck)
        {
            var parts = hashedPasswordWithSalt.Split(".");
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            var hashToCheck = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: passwordToCheck,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 210_000,
                numBytesRequested: 256 / 8
            ));

            return hashToCheck == storedHash;
        }
    }
}
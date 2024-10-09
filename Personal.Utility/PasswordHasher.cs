using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Utility
{
    public class PasswordHasher : IPasswordHasher
    {
        public (string Hash, string Salt) HashPassword(string password)
        {
            // Generate a 128-bit salt
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            // Derive a 256-bit subkey (using HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Convert the salt to a base64 string for storage
            string saltString = Convert.ToBase64String(salt);

            return (hashed, saltString);
        }

        public bool VerifyPassword(string storedHash, string storedSalt, string password)
        {
            // Convert the stored salt back to byte array
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            // Hash the input password using the same salt
            string hashedInputPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Compare the newly hashed input password with the stored hash
            return hashedInputPassword == storedHash;
        }
    }
}

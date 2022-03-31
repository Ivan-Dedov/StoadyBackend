using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Stoady.Services.Interfaces;

namespace Stoady.Services
{
    public sealed class PasswordValidatorService : IPasswordValidatorService
    {
        private const string LocalSalt = "HSECourseWorkStoady";
        private const int SaltLength = 32;

        public (string HashedPassword, string Salt) GetHashedPassword(
            string password)
        {
            var saltBytes = GenerateSaltBytes();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var localSaltBytes = Encoding.UTF8.GetBytes(LocalSalt);

            var hashAlgorithm = SHA256.Create();
            var saltyPassword = passwordBytes
                .Concat(saltBytes)
                .Concat(localSaltBytes);
            var hashedPasswordBytes = hashAlgorithm.ComputeHash(saltyPassword.ToArray());

            var salt = Convert.ToBase64String(saltBytes);
            var hashedPassword = Convert.ToBase64String(hashedPasswordBytes);

            return (hashedPassword, salt);
        }

        private static byte[] GenerateSaltBytes()
        {
            return RandomNumberGenerator.GetBytes(SaltLength);
        }

        public bool ValidatePassword(
            string password,
            string truePassword,
            string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var passwordStr = Encoding.UTF8.GetBytes(password)
                .Concat(saltBytes)
                .Concat(Encoding.UTF8.GetBytes(LocalSalt));

            var hashAlgorithm = SHA256.Create();
            var hashedPassword = Convert.ToBase64String(
                hashAlgorithm.ComputeHash(passwordStr.ToArray()));

            return hashedPassword == truePassword;
        }
    }
}

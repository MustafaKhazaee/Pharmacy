using System.Security.Cryptography;
using System.Text;
namespace Application.Extensions {
    public static class HashingExtensions {
        public static string GetHash(this string text) {
            using (var sha256 = SHA256.Create()) {
                var hashedBytes = sha256.ComputeHash(Encoding.Unicode.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        public static string AddSalt(this string text) => $"{text}{GetSalt(text)}";
        public static string GetSalt (this string a) {
            byte[] bytes = new byte[16];
            using (var keyGenerator = RandomNumberGenerator.Create()) {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}

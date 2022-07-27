using System.Text;

namespace FileShare.Utilities.Generators
{
    /// <summary>
    /// Static helper methods for generating strings.
    /// </summary>
    public static class RandomStringGenerator
    {
        /// <summary>
        /// Generate random base64 string.
        /// </summary>
        /// <param name="length">The length of the generated string. Defaults to 64.</param>
        /// <returns>A base64 encoded random string of characters.</returns>
        public static string Generate(int length = 64)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#¤%&/()=?~^*-_<>\\";
            Random random = new();

            var randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(randomString));
        }
    }
}
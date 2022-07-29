using FileShare.Utilities.Generators.Random.Interface;
using System.Text;

namespace FileShare.Utilities.Generators.Random
{
    /// <summary>
    /// Static helper methods for generating random stuff.
    /// </summary>
    public class RandomGenerator : IRandomGenerator
    {
        public string GenerateBase64String(int length = 64)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#¤%&/()=?~^*-_<>\\";
            System.Random random = new();

            var randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(randomString));
        }
    }
}
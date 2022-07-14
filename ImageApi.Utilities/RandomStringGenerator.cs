using System.Text;

namespace ImageApi.Utilities
{
    public static class RandomStringGenerator
    {
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
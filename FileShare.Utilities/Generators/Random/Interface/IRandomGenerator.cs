namespace FileShare.Utilities.Generators.Random.Interface
{
    public interface IRandomGenerator
    {
        /// <summary>
        /// Generate random base64 string.
        /// </summary>
        /// <param name="length">The length of the generated string. Defaults to 64.</param>
        /// <returns>A base64 encoded random string of characters.</returns>
        string GenerateBase64String(int length = 64);
    }
}
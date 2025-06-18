using System.Text;

namespace KkotipAppApi.Extensions;

public static class StringExtensions
{
    public static string ToHexString(this byte[] source)
    {
        return source.Select(x => x.ToString("X2")).Aggregate((x, y) => x + y).ToLower();
    }

    public static string RandomString(int length)
    {
        var alphabet = Enumerable.Range(0x0, 0xF).Select(x => x.ToString("X")).ToArray();

        var builder = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            builder.Append(alphabet[Random.Shared.Next(0, alphabet.Length)]);
        }

        return builder.ToString();
    }
}
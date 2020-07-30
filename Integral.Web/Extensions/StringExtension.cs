using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace Integral.Extensions
{
    public static class StringExtension
    {
        public static string Encode(this string value) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(value));

        public static string Decode(this string value) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(value));
    }
}

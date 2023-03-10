using System.Globalization;

namespace Dte.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string source)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(source.ToLower());
        }
    }
}
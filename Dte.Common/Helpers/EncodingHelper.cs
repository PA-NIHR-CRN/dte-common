using System;
using System.Text;

namespace Dte.Common.Helpers
{
    public static class EncodingHelper
    {
        public static string EncodeBase64(string source)
        {
            return string.IsNullOrWhiteSpace(source) ? null : Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
        }
        
        public static string DecodeBase64(string source)
        {
            return string.IsNullOrWhiteSpace(source) ? null : Encoding.UTF8.GetString(Convert.FromBase64String(source));
        }
    }
}
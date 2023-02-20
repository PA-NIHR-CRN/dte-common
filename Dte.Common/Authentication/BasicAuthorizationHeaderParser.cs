using System;
using System.Text;

namespace Dte.Common.Authentication
{
    public class BasicAuthorizationHeaderParser
    {
        public static BasicAuthorizationHeader GetHeader(string headerValue)
        {
            if (string.IsNullOrEmpty(headerValue))
            {
                return null;
            }
            
            var source = headerValue.Split(new[] { ' ' });

            if (source.Length != 2)
            {
                return null;
            }
            
            var scheme = source[0];
            
            if (scheme != "Basic")
            {
                return null;
            }
            
            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(source[1]);
            }
            catch (FormatException)
            {
                return null;
            }

            var encoding = (Encoding)Encoding.ASCII.Clone();
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedBase64String;
            try
            {
                decodedBase64String = encoding.GetString(bytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (string.IsNullOrEmpty(decodedBase64String))
            {
                return null;
            }
            
            var length = decodedBase64String.IndexOf(':');
            
            if (length == -1)
            {
                return null;
            }
            
            var userName = decodedBase64String[..length];
            var password = decodedBase64String[(length + 1)..];

            return new BasicAuthorizationHeader
            {
                Username = userName,
                Password = password
            };
        }
    }
}
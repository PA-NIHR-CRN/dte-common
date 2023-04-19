using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dte.Common.Helpers
{
    public static class PasswordHelper
    {
        private static readonly char[] SymbolList =
        {
            '^', '$', '*', '.', ',', '[', ']', '{', '}', '(', ')', '?', '"', '!', '@', '#', '%', '&', '/', '\\', ',',
            '>', '<', '\'', ':', ';', '|', '_', '~', '`', '=', '+', '-'
        };

        private static readonly string[] WeakPasswords =
        {
            "password",
            "000000",
            "123456",
            "12345678",
            "admin",
            "qwerty",
            "letmein",
            "football",
            "iloveyou",
            "admin123",
            "welcome",
            "monkey",
            "abc123",
            "login",
            "passw0rd",
            "password1",
            "123123",
            "trustno1",
            "shadow",
            "master",
            "hello",
            "solo",
            "princess",
            "welcome1",
            "qwertyuiop",
            "sunshine",
            "superman",
            "1234567890",
            "starwars",
            "iloveyou1",
            "letmein1",
            "letmein123",
            "baseball",
            "jennifer",
            "harley",
            "charlie",
            "michael",
            "ferrari",
            "mercedes",
            "mustang",
            "dragon",
            "pokemon",
            "abcdef",
            "aaaaaa",
            "bbbbbb",
            "cccccc",
            "dddddd",
            "eeeeee",
            "ffffff",
            "ghijkl",
            "hijklm",
            "ijklmn",
            "jklmno",
            "klmnop",
            "mnopqr",
            "nopqrs",
            "opqrst",
            "pqrstuv",
            "qrstuvw",
            "rstuvwx",
            "stuvwxy",
            "tuvwxyz",
            "password123",
            "adminadmin",
            "admin1234",
            "adminpassword",
            "123456a",
            "123456789a",
            "1234567a",
            "12345a",
            "1234a",
            "123a",
            "password12",
            "password1234",
            "password12345",
            "password123456",
            "password1234567",
            "password12345678",
            "password123456789",
            "qwerty123",
            "qwerty1234",
            "qwerty12345",
            "qwerty123456",
            "qwerty1234567",
            "qwerty12345678",
            "qwerty123456789",
            "letmein1234",
            "letmein12345",
            "letmein123456",
            "letmein1234567",
            "letmein12345678",
            "letmein123456789",
            "welcome123",
            "welcome1234",
            "welcome12345",
            "welcome123456",
            "welcome1234567",
            "welcome12345678",
            "welcome123456789",
            "admin12345",
            "admin123456",
            "admin1234567",
            "admin12345678",
            "admin123456789",
            "admin1234567890",
            "1q2w3e4r5t",
        };

        public static IEnumerable<string> PasswordRequirements(string password,
            int minimumLength = 8,
            bool requireLowercase = true,
            bool requireNumbers = true,
            bool requireSymbols = true,
            bool requireUppercase = true)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(password) || password.Length < minimumLength)
            {
                errors.Add("The password minimum length 8");
                return errors;
            }

            if (requireNumbers && !Regex.Match(password, "[0-9]+").Success)
            {
                errors.Add("The password requires numbers");
            }

            if (requireLowercase && !Regex.Match(password, "[a-z]+").Success)
            {
                errors.Add("The password requires lowercase");
            }

            if (requireUppercase && !Regex.Match(password, "[A-Z]+").Success)
            {
                errors.Add("The password requires uppercase");
            }

            if (requireSymbols && !password.Any(x => SymbolList.Contains(x)))
            {
                errors.Add("The password requires symbols");
            }
            
            if (WeakPasswords.Contains(password.ToLower()))
            {
                errors.Add("The password is too weak");
            }

            return errors;
        }
    }
}
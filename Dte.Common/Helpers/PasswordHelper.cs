using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dte.Common.Helpers
{
    public static class PasswordHelper
    {
        public static readonly char[] SymbolList =
        {
            '^', '$', '*', '.', ',', '[', ']', '{', '}', '(', ')', '?', '"', '!', '@', '#', '%', '&', '/', '\\', ',',
            '>', '<', '\'', ':', ';', '|', '_', '~', '`', '=', '+', '-'
        };

        public static readonly string[] WeakPasswords =
        {
            "password",
            "qwerty",
            "letmein",
            "welcome",
            "monkey",
            "sunshine",
            "superman",
            "starwars",
            "harley",
            "charlie",
            "michael",
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
            "chocolate",
            "football",
            "iloveyou",
            "computer",
            "baseball",
            "jennifer",
            "master",
            "hello",
            "freedom",
            "princess",
            "trustnoone",
            "batman",
            "superuser",
            "internet",
            "passwords",
            "whatever",
            "mercedes",
            "blessing",
            "amazing",
            "victoria",
            "robert",
            "matthew",
            "jessica",
            "angelina",
            "prince",
            "liverpool",
            "paradise",
            "marshall",
            "adidas",
            "martinez",
            "tinkerbell",
            "beautiful",
            "merlin",
            "america",
            "barcelona",
            "champion",
            "cocacola",
            "colorado",
            "christian",
            "harrison",
            "ilovegod",
            "maverick",
            "pakistan",
            "spiderman",
            "christina",
            "bananas",
            "bubbles",
            "francesco",
            "liverpool",
            "passwordword",
            "diamond",
            "yellow",
            "spider",
            "sweetie",
            "daniela",
            "thunder",
            "penelope",
            "aaron",
            "brittany",
            "washington",
            "dreamer",
            "butterfly",
            "madison",
            "republic",
            "angelica",
            "montana",
            "carolina",
            "shopping",
            "pumpkin",
            "lauren",
            "friends",
            "antonio",
            "alexander",
            "thunderbird",
            "lovelife",
            "lovelove",
            "trinity",
            "chelsea",
            "lakers",
            "loveme",
            "samantha",
            "patrick",
            "lovely",
            "alexandra",
            "mariposa",
            "joshua",
            "chocolatey",
            "midnight",
            "dolphins",
            "hawaii",
            "loveyou",
            "virginia",
            "charmed",
            "melissa",
            "colombia",
            "godzilla",
            "chester",
            "genesis",
            "elizabeth",
            "josephine",
            "derrick",
            "angelito",
            "flower",
            "forever",
            "soccer",
            "honeybee",
            "paradise",
            "columbus",
            "maddison",
            "panther",
            "phillip",
            "summer",
            "scarface",
            "douglas",
            "sweet",
            "valentina",
            "william",
            "savannah",
            "gateway",
            "happy",
            "creative",
            "freddie",
            "light",
            "alicia",
            "snowball",
            "vanessa",
            "passwordpass",
            "kenneth",
            "hershey",
        };
        
        private static bool IsCommonPassword(string password)
        {
            var strippedPassword = Regex.Replace(password, "[^a-zA-Z]", "");
            return WeakPasswords.Contains(strippedPassword.ToLower());
        }

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

            if (IsCommonPassword(password))
            {
                errors.Add("The password is too weak");
            }

            return errors;
        }
    }
}
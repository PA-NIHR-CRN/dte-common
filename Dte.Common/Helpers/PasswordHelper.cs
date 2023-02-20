using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dte.Common.Helpers
{
    public static class PasswordHelper
    {
        public static readonly char[] SymbolList = {'^', '$', '*', '.', ',', '[', ']', '{', '}', '(', ')', '?', '"', '!', '@', '#', '%', '&', '/', '\\', ',', '>', '<', '\'', ':', ';', '|', '_', '~', '`', '=', '+', '-'};

        public static IEnumerable<string> PasswordRequirements(string password,
            int minimumLength = 8,
            bool requireLowercase = true,
            bool requireNumbers = true,
            bool requireSymbols = true,
            bool requireUppercase = true)
        {
            var errors = new List<string>();
            if(string.IsNullOrWhiteSpace(password) || password.Length < minimumLength)
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

            return errors;
        }    
    }
}
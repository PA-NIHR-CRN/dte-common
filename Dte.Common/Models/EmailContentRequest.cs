using System.Globalization;

namespace Dte.Common.Models
{
    public class EmailContentRequest
    {
        public string EmailName { get; set; }
        public CultureInfo SelectedLocale { get; set; }
        public string Link { get; set; }
        public string FirstName { get; set; }
        public string Code { get; set; }
        public string InvisibleCognitoCodeParameter { get; set; } = string.Empty;
    }
}

using System.Collections.Generic;

namespace Dte.Common.Authentication
{
    public class BasicAuthenticationOptions
    {
        public BasicAuthenticationOptions()
        {
            Passwords = new List<string>();
        }
        
        public string Username { get; set; }
        public List<string> Passwords { get; set; }
    }
}
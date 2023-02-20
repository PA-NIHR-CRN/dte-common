namespace Dte.Common.Authentication
{
    public class AuthenticationSettings
    {
        public static string SectionName => "AuthenticationSettings";
        public BasicAuthClient[] BasicAuthClients { get; set; }
    }
    
    public class BasicAuthClient
    {
        public string ClientName { get; set; }
        public string ClientPassword { get; set; }
    }
}
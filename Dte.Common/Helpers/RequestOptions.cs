namespace Dte.Common.Helpers
{
    public class RequestOptions
    {
        public bool IncludeConversationIdHeader { get; set; } = true;
        public bool IncludeUserAgentHeader { get; set; } = true;
        public bool IncludeAuthorizationHeader { get; set; } = true;
        public bool IncludeVersionHeader { get; set; } = false;
    }
}
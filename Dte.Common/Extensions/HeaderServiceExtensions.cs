using System.Linq;
using Dte.Common.Http;

namespace Dte.Common.Extensions
{
    public static class HeaderServiceExtensions
    {
        public static string GetConversationId(this IHeaderService headerService) => headerService.GetHeader(DefaultHeaders.ConversationId).FirstOrDefault();
        public static string GetUserAgent(this IHeaderService headerService) => headerService.GetHeader(DefaultHeaders.UserAgent).FirstOrDefault();
    }
}
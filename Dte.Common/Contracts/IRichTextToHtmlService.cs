using Dte.Common.Models;

namespace Dte.Common.Contracts
{
    public interface IRichTextToHtmlService
    {
        string Convert(RichTextNode richTextNode, string baseUrl);
    }
}
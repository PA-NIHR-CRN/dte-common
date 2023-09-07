using System.Globalization;
using System.Threading.Tasks;
using Dte.Common.Models;

namespace Dte.Common.Contracts
{
    public interface IContentfulService
    {
        Task<ContentfulEmail> GetContentfulEmailAsync(string entryId, CultureInfo locale);
        Task<ContentfulEmailResponse> GetEmailContentAsync(EmailContentRequest request);
    }
}
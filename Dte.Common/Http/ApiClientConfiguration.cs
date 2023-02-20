using Dte.Common.Contracts;
using Dte.Common.Helpers;

namespace Dte.Common.Http
{
    public class ApiClientConfiguration
    {
        public static ApiClientConfiguration Default => new ApiClientConfiguration
        {
            TraceWriter = new ConsoleTraceWriter(),
            ContractPropertyCase = ContractPropertyCase.None
        };

        public ITraceWriter TraceWriter { get; set; }
        public ContractPropertyCase ContractPropertyCase { get; set; }
    }
}
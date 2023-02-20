namespace Dte.Common.Contracts
{
    public interface ITraceWriter
    {
        void WriteLine(string message = null);
    }
}
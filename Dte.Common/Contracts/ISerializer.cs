namespace Dte.Common.Contracts
{
    public interface ISerializer
    {
        string Serialize<TRequest>(TRequest request);
        void TryDeserialize<TResponse>(string stringContent, out TResponse content);
    }
}
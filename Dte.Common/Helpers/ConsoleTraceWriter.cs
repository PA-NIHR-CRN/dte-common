using System;
using Dte.Common.Contracts;

namespace Dte.Common.Helpers
{
    public class ConsoleTraceWriter : ITraceWriter
    {
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}
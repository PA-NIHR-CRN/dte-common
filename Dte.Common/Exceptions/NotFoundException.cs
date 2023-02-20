using System;

namespace Dte.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Could not find {name} : ({key})") { }
        public NotFoundException(string message) : base(message) { }
    }
}
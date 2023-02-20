using System;

namespace Dte.Common.Exceptions
{
    internal class PropertyNameCasingException : Exception
    {
        public PropertyNameCasingException(string message) : base(message)
        {
        }
    }
}
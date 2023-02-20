using System;
using Dte.Common.Contracts;

namespace Dte.Common
{
    public class Clock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
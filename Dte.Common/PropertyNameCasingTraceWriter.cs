using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Dte.Common.Exceptions;

namespace Dte.Common
{
    internal class PropertyNameCasingTraceWriter : Newtonsoft.Json.Serialization.ITraceWriter
    {
        private static readonly Regex PropertyPathRegEx = new Regex("Path '(?<propertyPath>\\w+)':", RegexOptions.Compiled);
        private readonly string _caseName;
        private readonly Regex _caseRegEx;

        public PropertyNameCasingTraceWriter(string caseName, Regex caseRegEx)
        {
            _caseName = caseName;
            _caseRegEx = caseRegEx;
        }

        public void Trace(TraceLevel level, string message, Exception ex)
        {
            var str = PropertyPathRegEx.Match(message).Groups["propertyPath"].Value;
            var input = str.Split(new char[1] {'.' }).Last();
            
            if (!string.IsNullOrWhiteSpace(input) && !_caseRegEx.IsMatch(input))
            {
                throw new PropertyNameCasingException("An exception has occurred deserialising response from the remote uri. Property '" + str + "' was expected to be " + this._caseName);
            }
        }

        public TraceLevel LevelFilter => TraceLevel.Verbose;
    }
}
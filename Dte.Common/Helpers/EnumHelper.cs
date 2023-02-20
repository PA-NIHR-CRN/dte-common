using System;

namespace Dte.Common.Helpers
{
    public static class EnumHelper
    {
        public static TEnum[] GetEnumArray<TEnum>() where TEnum : Enum => ((TEnum[])Enum.GetValues(typeof(TEnum)));
    }
}
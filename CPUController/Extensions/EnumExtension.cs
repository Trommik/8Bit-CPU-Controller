using System;

namespace CPUController.Extensions
{
    internal static class EnumExtension
    {
        internal static bool Contains<TEnum>(byte enumValue)
        {
            return Enum.IsDefined(typeof(TEnum), enumValue);
        }

        internal static bool Contains<TEnum>(string enumValue)
        {
            return Enum.IsDefined(typeof(TEnum), enumValue);
        }
    }
}
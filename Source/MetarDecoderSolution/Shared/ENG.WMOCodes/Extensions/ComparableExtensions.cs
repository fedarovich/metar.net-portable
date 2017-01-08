using System;

namespace ENG.WMOCodes.Extensions
{
    internal static class ComparableExtensions
    {
        public static bool IsBetween<T>(this IComparable<T> value, T firstBound, T secondBound) where T : IComparable<T>
        {
            if (firstBound.CompareTo(secondBound) > 0)
            {
                T obj = firstBound;
                firstBound = secondBound;
                secondBound = obj;
            }
            return value.CompareTo(firstBound) >= 0 && value.CompareTo(secondBound) <= 0;
        }
    }
}

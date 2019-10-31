using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ENG.WMOCodes.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAssignableFrom(this Type type, Type c)
        {
            if (c == null)
                throw new ArgumentNullException(nameof(c));
            return type.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }

        public static bool IsAssignableFrom<T>(this Type type)
        {
            return type.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo());
        }

        public static bool IsAssignableTo<T>(this Type type) => typeof(T).IsAssignableFrom(type);
    }
}

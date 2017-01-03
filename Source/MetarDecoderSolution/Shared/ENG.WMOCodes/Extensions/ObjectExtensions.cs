using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ENG.WMOCodes.Extensions
{
    internal static class ObjectExtensions
    {
        private static int callIndex = 0;

        public static string ToInlineInfoString(this object obj)
        {
            ++callIndex;
            if (obj == null)
                return "(null)";
            if (obj is string)
                return Convert.ToString(obj);
            StringBuilder builder = new StringBuilder();
            StringBuilder stringBuilder = new StringBuilder();
            Type type = obj.GetType();
            if (obj is ICollection)
            {
                foreach (object item in (IEnumerable)obj)
                {
                    string text = ToInlineInfoString(item);
                    builder.AppendPreDelimited(text, ';');
                }
            }
            else if (obj is IEnumerable)
            {
                foreach (object item in (IEnumerable)obj)
                {
                    string text = ToInlineInfoString(item);
                    builder.AppendPreDelimited(text, ';');
                }
            }
            else
            {
                var properties = type.GetRuntimeProperties();
                string str = GenerateInlineContent(obj, properties);
                if (str.Length > 0)
                {
                    builder.AppendPreDelimited(str, ';');
                }
            }
            if (builder.Length == 0 && !(obj is ICollection))
            {
                stringBuilder.Append(obj);
            }
            else
            {
                stringBuilder.Append("{" + builder + "}");
                stringBuilder.Insert(0, "(" + type.Name + ")");
            }
            return stringBuilder.ToString();
        }

        private static string GenerateInlineContent(object obj, IEnumerable<PropertyInfo> properties)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var propertyInfo in properties)
            {
                string str = ToInlineInfoString(propertyInfo.GetValue(obj, null));
                builder.AppendPreDelimited(propertyInfo.Name + "=" + str, ',');
            }
            return builder.ToString();
        }
    }
}

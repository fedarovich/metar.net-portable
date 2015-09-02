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
        private const string NULL = "(null)";
        private const string INTENDER = "  ";
        private const string DEFAULT_ENCAPSULATING_TAG = "This";
        private const string DEFAULT_ENUMERATIONITEM_TAG = "Item";
        private const string DEFAULT_NULL_TAGITEM = "(null)";

        public static string ToInlineInfoString(this object obj)
        {
            ++callIndex;
            if (callIndex > 100)
                Console.WriteLine("stop");
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
                MemberInfo[] members = type.GetProperties().Cast<MemberInfo>().ToArray();
                string str = GenerateInlineContent(obj, members);
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

        private static string GenerateInlineContent(object obj, MemberInfo[] members)
        {
            StringBuilder builder = new StringBuilder();
            foreach (MemberInfo memberInfo in members)
            {
                string str = ToInlineInfoString(obj.GetType().InvokeMember(memberInfo.Name, BindingFlags.GetProperty, (Binder)null, obj, (object[])null));
                builder.AppendPreDelimited(memberInfo.Name + "=" + str, ',');
            }
            return builder.ToString();
        }
    }
}

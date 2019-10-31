using System;
using System.Text;

namespace ENG.WMOCodes.Extensions
{
    internal static class StringBuilderExtensions
    {
        public static void AppendPreSpaced(this StringBuilder builder, string text)
        {
            AppendPreDelimited(builder, text, " ");
        }

        public static void AppendPreDelimited(this StringBuilder builder, string text, char preDelimiter)
        {
            AppendPreDelimited(builder, text, char.ToString(preDelimiter));
        }

        public static void AppendPreDelimited(this StringBuilder builder, string text, string preDelimiter)
        {
            if (builder.Length > 0 && !builder.EndsWith(preDelimiter))
                builder.Append(preDelimiter);
            builder.Append(text);
        }

        public static void AppendPostSpaced(this StringBuilder builder, string text)
        {
            AppendPostDelimited(builder, text, " ");
        }

        public static void AppendPostDelimited(this StringBuilder builder, string text, char postDelimiter)
        {
            AppendPostDelimited(builder, text, char.ToString(postDelimiter));
        }

        public static void AppendPostDelimited(this StringBuilder builder, string text, string postDelimiter)
        {
            builder.Append(text);
            if (builder.Length <= 0 || builder.EndsWith(postDelimiter))
                return;
            builder.Append(postDelimiter);
        }

        public static bool EndsWith(this StringBuilder builder, string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            return builder.ToString().EndsWith(text);
        }
    }
}

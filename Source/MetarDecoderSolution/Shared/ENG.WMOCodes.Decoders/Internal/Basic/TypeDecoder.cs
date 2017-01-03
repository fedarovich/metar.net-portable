using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Decoders.Internal.Basic
{
    internal abstract class TypeDecoder<T> : InternalDecoder<T>
    {
        public abstract override string Description { get; }

        public abstract string RegEx { get; }

        protected sealed override T _Decode(ref string source)
        {
            T ret = default(T);

            var groups = TryGetGroups(ref source);
            if (groups != null)
                ret = _Decode(groups);
            else
              if (Required)
                throw
                  new DecodeException(Description,
                    new ArgumentException("Failed text is >" + source + "<."));
            else
            {
                ret = typeof(T).IsAssignableTo<System.Collections.IList>()
                    ? Activator.CreateInstance<T>()
                    : default(T);
            }

            return ret;
        }

        protected abstract T _Decode(GroupCollection groups);

        protected GroupCollection TryGetGroups(ref string source)
        {
            GroupCollection ret = null;
            Match m = Regex.Match(source, RegEx);

            if (m.Success)
            {
                ret = m.Groups;
                source = source.Substring(ret[0].Length).TrimStart();
            }

            return ret;
        }
    }
}

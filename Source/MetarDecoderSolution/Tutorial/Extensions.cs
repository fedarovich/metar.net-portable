using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial
{
  static class Extensions
  {
    /// <summary>
    /// Returns recursively all messages of exception and its inner exceptions.
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static string GetMessages (this Exception ex)
    {
      StringBuilder ret = new StringBuilder();

      while (ex != null)
      {
        if (ret.Length > 0)
          ret.Append(" --> ");

        ret.Append(ex.Message);

        ex = ex.InnerException;
      }

      return ret.ToString();
    }
  }
}

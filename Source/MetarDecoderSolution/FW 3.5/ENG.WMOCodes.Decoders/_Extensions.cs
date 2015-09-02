using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace ENG.WMOCodes.Decoders
{
  static class _Extensions
  {
    /// <summary>
    /// Adds string into string builder and then empty space at the end.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="text"></param>
    public static void AppendSpaced(this System.Text.StringBuilder builder, string text)
    {
      builder.Append(text);
      if ((!string.IsNullOrEmpty(text)) && (!text.EndsWith(" ")))
        builder.Append(" ");
    }

    /// <summary>
    /// Returns value from Group parsed as integer. Exception if fails.
    /// </summary>
    /// <param name="grp"></param>
    /// <returns></returns>
    public static int GetIntValue(this System.Text.RegularExpressions.Group grp)
    {
      int ret =
        int.Parse(grp.Value);
      return ret;
    }

    /// <summary>
    /// Copies the properties from one object to the other. Only intersection of the properties are copied, rest is ignored.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public static void CopyPropertiesTo(this object source, object target)
    {
      CopyPropertiesTo(source, target, new List<string>());
    }

    public static void CopyPropertiesTo(this object source, object target, params string [] excludes)
    {
      List<string> excludeLst = excludes.ToList();

      CopyPropertiesTo(source, target, excludeLst);
    }

    /// <summary>
    /// Copies the properties from one object to the other. Only intersection of the properties are copied, rest is ignored.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="excludeList">The list of properties which should be skipped and not copied..</param>
    public static void CopyPropertiesTo(this object source, object target, List<string> excludeList)
    {
      var ins = source.GetType().GetInterfaces();
      bool areLists = false;
      areLists = IsList(source) && IsList(target);

      PropertyInfo[] sp = source.GetType().GetProperties();
      PropertyInfo[] tp = target.GetType().GetProperties();
      PropertyInfo[] shared = GetSharedProperties(sp, tp);
      CutOutExcluded(ref shared, excludeList);
      object val;

      foreach (var fItem in shared)
      {
        try
        {
          val = fItem.GetValue(source, null);
          fItem.SetValue(target, val, null);
        }
        catch (Exception ex)
        {
          throw new Exception("Failed to copy property " + fItem.Name + " of type " + source.GetType().FullName + ".", ex);
        }
      } // foreach (var fItem in shared)

      try
      {
        if (areLists)
          CopyListItems(source as IList, target as IList);
      } // try
      catch (Exception ex)
      {
        throw new Exception("Failed to copy items between ILists.", ex);
      } // catch (Exception ex)
    }

    private static void CutOutExcluded(ref PropertyInfo[] shared, List<string> excludeList)
    {
      List<PropertyInfo> ret = new List<PropertyInfo>();

      foreach (var fItem in shared)
      {
        if (excludeList.Contains(fItem.Name) == false)
          ret.Add(fItem);
      } // foreach (var fItem in shared)

      shared = ret.ToArray();
    }

    private static void CopyListItems(IList source, IList target)
    {
      foreach (var fItem in source)
      {
        target.Add(fItem);
      } // foreach (var fItem in source)
    }

    private static bool IsList(object target)
    {
      foreach (var fItem in target.GetType().GetInterfaces())
      {
        if (fItem.FullName.Contains("System.Collections.Generic.IList"))
          return true;
      } // foreach (var fItem in ins)
      return false;
    }

    private static PropertyInfo[] GetSharedProperties(PropertyInfo[] sp, PropertyInfo[] tp)
    {
      List<PropertyInfo> ret = new List<PropertyInfo>();
      foreach (var fS in sp)
        if (fS.GetIndexParameters().Length == 0)
        {
          foreach (var fT in tp)
            if (fS.Name == fT.Name && fS.CanRead && fT.CanWrite) ret.Add(fS);
        }
      return ret.ToArray();
    }
  }
}

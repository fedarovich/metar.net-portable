using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents recent phenom info. Derived from <see cref="PhenomInfo"/>.
  /// </summary>
  public class RePhenomInfo : PhenomInfo
  {
    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
        StringBuilder ret = new StringBuilder();

        this.ForEach(
          i => ret.AppendPostSpaced("RE" + i.ToCode()));

        return ret.ToString().TrimEnd();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
    public override string ToString()
    {
      return ESystem.Extensions.ObjectExt.ToInlineInfoString(this);
    }

    #endregion Inherited
  }
}

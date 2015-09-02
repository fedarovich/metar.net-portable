using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents information about phenoms. E.g. (+RAHZ -SN)
  /// </summary>
  public class PhenomInfo : List<ePhenomCollection>, ICodeItem
  {    

    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToCode()
    {
      
        StringBuilder ret = new StringBuilder();

        this.ForEach(
          i => ret.AppendPreSpaced(i.ToCode()));

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

    #region MetarItem Members

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do here
    }

    #endregion

    #endregion Inherited

    /// <summary>
    /// Returns true if any phenom collection contains selected phenom.
    /// </summary>
    /// <param name="phenom"></param>
    /// <returns></returns>
    public bool Contains(ePhenomCollection.ePhenom phenom)
    {
      bool ret = false;

      foreach (var fItem in this)
      {
        if (fItem.Contains(phenom))
        {
          ret = true;
          break;
        }
      } // foreach (var fItem in this)

      return ret;
    }

    internal virtual bool IsEmpty()
    {
      return (this.Count == 0);
    }
  }
}

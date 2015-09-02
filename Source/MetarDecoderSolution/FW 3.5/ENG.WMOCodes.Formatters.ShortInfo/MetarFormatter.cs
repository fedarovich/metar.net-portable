using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using R = ENG.WMOCodes.Formatters.ShortInfoFormatter.Properties.Resources;
using ENG.WMOCodes.Types;
using System.Globalization;

namespace ENG.WMOCodes.Formatters.ShortInfoFormatter
{
  /// <summary>
  /// Converts metar into short information string .
  /// </summary>
  public class MetarFormatter : ENG.WMOCodes.Formatters.IMetarFormatter
  {
    #region IMetarFormatter Members

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this instance.
    /// </summary>
    /// <param name="metar">The metar.</param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public string ToString(Codes.Metar metar)
    {
      StringBuilder ret = new StringBuilder();

      // EETN - 21:50Z: Wind N at 18 KT. Overacast. Heavy snow mist, light rain haze. Temperature: 4°C. Pressure 1013 hPa.
      // EETN - 21:50Z: Vítr N o rychlosti 18 uzlů. Zataženo. Silný sníh mlha, lehký déšŤ hajzl. Teplota XoC. Tlak vzduchu 1013 hPa.

      ret.Append(metar.ICAO);

      ret.Append(R.Space + R.Dash + R.Space);

      ret.Append(GetTimeString(metar));

      ret.Append(R.Colon +  R.Space);

      ret.Append(
        GetWindString(metar.Wind));

      ret.Append(R.Space);

      ret.Append(GetCloudLevelString(metar.Clouds));

      ret.Append(R.Space);

      ret.Append(GetPhenomensString(metar.Phenomens));

      ret.Append(R.Space);

      ret.Append(GetTemperatureString(metar));      

      ret.Append(R.Space);

      ret.Append(GetPressureString(metar));      

      NormalizeResult(ret);

      return ret.ToString();
    }

    /// <summary>
    /// Returns a <see cref="System.String"/> that represents this instance.
    /// </summary>
    /// <param name="metar">The metar.</param>
    /// <param name="cultureInfo">The culture info.</param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public string ToString(Codes.Metar metar, CultureInfo cultureInfo)
    {
      var oldCulture = R.Culture;
      R.Culture = cultureInfo;

      string ret = ToString(metar);

      R.Culture = oldCulture;

      return ret;
    }

    #region Time

    private string GetTimeString(Codes.Metar metar)
    {
      return metar.Date.Hour.ToString("0") +
              R.Colon + metar.Date.Minute.ToString("00") + R.Zulu;
    }
    #endregion Time

    #region Pressure

    private string GetPressureString (Codes.Metar metar)
    {
      return R.PressurePrefix + R.Colon + R.Space + GetPressureString(metar.Pressure) + GetPressureUnitString(metar.Pressure) + R.Dot;
    }

    private string GetPressureUnitString(PressureInfo pressureInfo)
    {
      switch (pressureInfo.Unit)
      {
        case PressureInfo.eUnit.hPa:
          return R.HPA;
        case PressureInfo.eUnit.mmHq:
          return R.MMHQ;
        default:
          throw new NotSupportedException();
      }
    }

    private string GetPressureString(PressureInfo pressureInfo)
    {
      return pressureInfo.QNH.ToString("0");
    }

    #endregion Pressure

    #region Temperature

    private string GetTemperatureString (Codes.Metar metar)
    {
      return  R.TemperaturePrefix  + R.Colon + R.Space  + GetTemperatureString(metar.Temperature) + R.TemperaturePostfix + R.Dot;
    }

    private string GetTemperatureString(int temperatureValue)
    {
      return temperatureValue.ToString();
    }

    #endregion Temperature

    #region Phenomens

    private string GetPhenomensString(PhenomInfo phenomInfo)
    {
      StringBuilder ret = new StringBuilder();

      foreach (var fItem in phenomInfo)
      {
        foreach (var yItem in fItem)
        {
          ret.AppendPostSpaced(GetPhenomString(yItem));
        } // foreach (var yItem in fItem)
        ret.AppendPostSpaced(R.Dot);
      } // foreach (var fItem in phenomInfo)

      return ret.ToString();
    }

    private string GetPhenomString(ePhenomCollection.ePhenom yItem)
    {
      string s = yItem.ToString();
      string ret = R.ResourceManager.GetString(s, R.Culture);
      return ret;
    }

    #endregion Phenomens

    #region Clouds

    private string GetCloudLevelString(CloudInfoWithNCD cloudInfoWithNCD)
    {
      string ret = null;
      Cloud.eType t;

      if (cloudInfoWithNCD.IsNCD || cloudInfoWithNCD.IsNSC || cloudInfoWithNCD.IsSKC || cloudInfoWithNCD.IsCLR || cloudInfoWithNCD.Count == 0)
        ret = R.NoClouds;
      else
      {
        t = GetMostClouds(cloudInfoWithNCD);
        ret = GetCloudTypeString(t);
      }

      ret += R.Dot;

      return ret;
    }

    private string GetCloudTypeString(Cloud.eType t)
    {
      string r = t.ToString();
      string ret = R.ResourceManager.GetString(r, R.Culture);
      return ret;
    }

    private Cloud.eType GetMostClouds(CloudInfoWithNCD cloudInfoWithNCD)
    {
      Cloud.eType t = (Cloud.eType)1;

      foreach (var fItem in cloudInfoWithNCD)
      {
        if (((int)fItem.Type) > (int)t)
          t = fItem.Type;
      } // foreach (var fItem in cloudInfoWithNCD)

      return t;
    }
    #endregion Clouds

    #region Wind

    private string GetSpeedUnitString(Types.Common.eSpeedUnit eSpeedUnit)
    {
      switch (eSpeedUnit)
      {
        case Types.Common.eSpeedUnit.kph:
          return R.KPH;
        case Types.Common.eSpeedUnit.kt:
          return R.KT;
        case Types.Common.eSpeedUnit.miph:
          return R.MIPH;
        case Types.Common.eSpeedUnit.mps:
          return R.MPS;
        default:
          return "???";
      }
    }

    private string GetWindString(Wind w)
    {
      StringBuilder ret = new StringBuilder();

      if (w.IsCalm)
        ret.AppendPreSpaced(R.WindCalm);
      else
      {
        ret.Append(R.WindPrefix);

        ret.Append(R.Space);

        if (w.IsVariable)
          ret.AppendPreSpaced(R.WindVariable);
        else
          ret.Append(GetWindDirectionString(w.WorldDirection));

        ret.Append(R.Space);

        ret.Append(R.WindSpeedPrefix);

        ret.Append(R.Space);

        ret.Append(w.Speed.ToString());

        ret.Append(R.Space);

        ret.Append(GetSpeedUnitString(w.Unit));
      }

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private string GetWindDirectionString(Common.eDirection d)
    {
      string s = d.ToString();
      string ret = R.ResourceManager.GetString(s, R.Culture);
      return ret;
    }
    #endregion Wind

    #endregion

    private void NormalizeResult(StringBuilder sb)
    {
      RemoveSpaces(sb);
      ChangeCapitalLetters(sb);
      AddSpaces(sb);
    }

    private void ChangeCapitalLetters(StringBuilder sb)
    {
      for (int i = 0; i < sb.Length - 1; i++)
      {
        if (IsPunctuation(sb[i]))
          sb[i + 1] = char.ToUpper(sb[i + 1]);
      }
    }

    char[] pncs = new char[] { '.', ',', ':' };
    private bool IsPunctuation(char p)
    {
      if (pncs.Contains(p))
        return true;
      else
        return false;
    }

    private void AddSpaces(StringBuilder sb)
    {
      sb.Replace(".", ". ");
      sb.Replace(",", ", ");
    }

    private void RemoveSpaces(StringBuilder sb)
    {
      DoReplace(sb, " .", ".");
      DoReplace(sb, ". ", ".");
      DoReplace(sb, " ,", ",");
      DoReplace(sb, ", ", ",");

      DoReplace(sb, "  ", " ");
    }

    private void DoReplace(StringBuilder sb, string original, string replacing)
    {
      while (sb.ToString().Contains(original))
        sb.Replace(original, replacing);
    }
  }
}

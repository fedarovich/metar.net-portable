using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using ESystem.Extensions;
using R = ENG.WMOCodes.Formatters.InfoFormatter.Properties.Resources;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Codes;

namespace ENG.WMOCodes.Formatters.InfoFormatter
{
  class Formatter
  {
    internal static string ToString(Codes.Taf taf)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.TAF + R.Space);
      ret.Append(R.For + R.Space);
      ret.Append(taf.ICAO + R.Space);
      ret.Append(R.IssuedAtDay + R.Space);
      ret.Append(Get(taf.DayTime) + R.Dot);
      if (taf.IsMissing)
        ret.Append(R.Missing + R.Dot);
      else
      {
        ret.Append(Get(taf.Period));

        if (taf.IsCancelled)
          ret.Append(R.Cancelled + R.Dot);
        else
        {
          ret.Append(Get(taf as TrendReport));
          //ret.Append(Get(taf.Wind) + R.Dot);
          //ret.Append(Get(taf.Visibility) + R.Dot);
          //ret.Append(Get(taf.Phenomens) + R.Dot);
          //ret.Append(Get(taf.Clouds) + R.Dot);

          ret.Append(GetTemperatures(taf) + R.Dot);

          foreach (var fItem in taf.Trends)
            ret.Append(Get(fItem) + R.Dot);
        }
      }

      Reformat(ret);

      return ret.ToString();
    }

    private static string Get (TrendInfoForTaf trendInfoForTaf)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(Get(trendInfoForTaf.Interval) + R.Dot);

      ret.Append(Get(trendInfoForTaf as TrendReport) + R.Dot);

      return ret.ToString();
    }

    private static string Get(TafInterval tafInterval)
    {
      string ret;
      if (tafInterval is TafIntervalFM)
        ret = Get(tafInterval as TafIntervalFM);
      else
        ret = Get(tafInterval as TafIntervalNonFM);

      return ret;
    }

    private static string Get(TafIntervalFM tafInterval)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.NextWeatherIsExpected + R.Space + R.From + 
        R.Space + Get(tafInterval.From) + R.Space + R.Dot);

      return ret.ToString();
    }

    private static string Get(TafIntervalNonFM tafInterval)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.NextWeatherIsExpected + R.Space);
      switch (tafInterval.Type)
      {
        case TafIntervalNonFM.eType.BECMG:
          break;
        case TafIntervalNonFM.eType.INTER:
          break;
        case TafIntervalNonFM.eType.PROB30:
          ret.Append (R.Prob30 + R.Space);
          break;
        case TafIntervalNonFM.eType.PROB40:
          ret.Append (R.Prob40 + R.Space);
          break;
        case TafIntervalNonFM.eType.TEMPO:
          ret.Append (R.TEMPO + R.Space);
          break;
        case TafIntervalNonFM.eType.TEMPO_PROB30:
          ret.Append (R.TEMPO + R.Space + R.Prob30 + R.Space);
          break;
          case TafIntervalNonFM.eType.TEMPO_PROB40:
          ret.Append (R.TEMPO + R.Space + R.Prob40 + R.Space);
          break;
        default:
          break;
      }
      
      ret.Append (Get(tafInterval.Interval) + R.Dot);

      return ret.ToString();
    }

    #region Taf expected temperatures

    private static string GetTemperatures(Taf taf)
    {
      StringBuilder sb = new StringBuilder();

      if (taf.MaxTemperature != null)
        sb.Append(Get(taf.MaxTemperature));
      sb.Append(R.Space + R.Dot);
      if (taf.MinTemperature != null)
        sb.Append(Get(taf.MinTemperature));
      sb.Append(R.Dot);

      return sb.ToString();
    }

    private static string Get(TemperatureExtremeTN temperatureExtremeTN)
    {
      return R.Min + R.Space + R.Temperature + R.Colon + R.Space + temperatureExtremeTN.Temperature + R.Celsius +
        R.Space + R.TemperatureAtTime + R.Space + Get(temperatureExtremeTN.Time);
    }

    private static string Get(TemperatureExtremeTX temperatureExtremeTX)
    {
      return R.Max + R.Space + R.Temperature + R.Colon + R.Space + temperatureExtremeTX.Temperature + R.Celsius +
        R.Space + R.TemperatureAtTime + R.Space + Get(temperatureExtremeTX.Time);
    }

    private static string Get(Types.DateTimeTypes.DayHourDayHour dayHourDayHour)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.ValidFrom +
        R.Space + Get(dayHourDayHour.From) + R.Space +  R.To + R.Space + Get(dayHourDayHour.To) + R.Dot);

      return ret.ToString();
    }

    #endregion Taf expected temperatures

    private static string Get(Types.DateTimeTypes.DayHour dayHour)
    {
      return R.Day + R.Space + dayHour.Day + R.Dot + R.Space + dayHour.Hour.ToString() + ":00" + R.Space;
    }

    internal static StringBuilder ToString(Codes.Metar metar)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(metar.Type == Metar.eType.METAR ? R.METAR : R.SPECI);
      ret.Append(R.Space);      

      ret.Append(R.For);
      ret.Append(R.Space);

      ret.Append(metar.ICAO);
      ret.Append(R.Space);

      ret.Append(R.IssuedAtDay);
      ret.Append(R.Space);

      ret.Append(Get(metar.Date));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Wind));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Visibility));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Phenomens));
      ret.Append(R.Dot);

      ret.Append(Get(metar.Clouds));
      ret.Append(R.Dot);

      ret.Append(R.Temperature + R.Colon + R.Space + metar.Temperature.ToString() + R.Celsius + R.Dot);
      ret.Append(R.DewPoint + R.Colon + R.Space + metar.DewPoint.ToString() + R.Celsius + R.Dot);

      ret.Append(Get(metar.Pressure));
      ret.Append(R.Dot);

      ret.Append(Get(metar.WindShears));
      ret.Append(R.Dot);

      ret.Append(Get(metar.RunwayConditions));
      ret.Append(R.Dot);

      if (metar.Trend != null)
        ret.Append(Get(metar.Trend));
      ret.Append(R.Dot);

      if (metar.Remark != null)
        ret.Append(GetRemark(metar.Remark));
      ret.Append(R.Dot);

      Reformat(ret);

      return ret;
    }

    private static string Get(TrendInfoForMetar trendInfoForMetar)
    {
      StringBuilder ret = new StringBuilder();

      if (trendInfoForMetar.Type == TrendInfoForMetar.eType.NOSIG)
        ret.Append(R.NOSIG);
      else
      {
        ret.Append(Get(trendInfoForMetar.Type) + R.Colon+ R.Space);

        ret.Append(
          Get(trendInfoForMetar as TrendReport));
      }

      return ret.ToString();
    }

    private static string Get(TrendReport trendReport)
    {
      StringBuilder ret = new StringBuilder();

      if (trendReport.Wind != null)
        ret.Append(Get(trendReport.Wind) + R.Dot);

      if (trendReport.Visibility != null)
        ret.Append(Get(trendReport.Visibility) + R.Dot);

      ret.Append(Get(trendReport.Phenomens) + R.Dot);

      ret.Append(Get(trendReport.Clouds) + R.Dot);

      return ret.ToString();
    }

    private static string Get(Visibility visibility)
    {
          StringBuilder ret = new StringBuilder();

          if (visibility.IsClear)
            ret.Append(R.VisibilityClear);
          else
          {
            ret.Append(R.Visibility + R.Space + Get(visibility.Distance.Value) + R.Space);
          }

        return ret.ToString();
    }

    private static string Get(TrendInfoForMetar.eType eType)
    {
      return
        R.ResourceManager.GetString(eType.ToString(), R.Culture);
    }

    private static string GetRemark(string p)
    {
      return R.Remark + R.Colon + R.Space + p;
    }

    #region Runway condition


    private static string Get(RunwayConditionInfo runwayConditionInfo)
    {
      StringBuilder ret = new StringBuilder();

      if (runwayConditionInfo.IsSNOCLO == false && runwayConditionInfo.Count == 0)
      { ; }
      else if (runwayConditionInfo.IsSNOCLO)
        ret.Append(R.SNOCLO);
      else
        foreach (var fItem in runwayConditionInfo)
          ret.Append(Get(fItem) + R.Semicolon);

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(RunwayCondition fItem)
    {
      StringBuilder ret = new StringBuilder();

      if (fItem.IsForAllRunways)
        ret.Append(R.AllRunways + R.Colon +  R.Space);
      else
        ret.Append(R.Runway + R.Space + fItem.Runway + R.Colon+ R.Space);

      if (fItem.IsCleared)
        ret.Append(R.RunwayIsCleared);
      else
      {

      if (fItem.Contamination.HasValue)
        ret.Append(R.CoveredBy + R.Space + Get(fItem.Contamination.Value));

      if (fItem.Depth.HasValue)
        ret.Append(Get(fItem.Depth.Value) + R.Comma);

      if (fItem.Deposit.HasValue)
        ret.Append(Get(fItem.Deposit.Value) + R.Comma);

      if (fItem.Friction.HasValue)
        ret.Append(R.BrakingAction + R.Space + Get(fItem.Friction.Value));
      }

      return ret.ToString();
    }

    private static string Get(RunwayCondition.eFriction eFriction)
    {
      string ret;
      if (eFriction < RunwayCondition.eFriction.BrakingActionPoor)
        ret = eFriction.ToString().Substring(10) + "%";
      else if (eFriction.ToString().StartsWith("Reserved"))
        ret = R.Unknown;
      else
        ret = R.ResourceManager.GetString(eFriction.ToString(), R.Culture);

      return ret;
    }

    private static string Get(RunwayCondition.eDeposit eDeposit)
    {
      string ret =
        R.ResourceManager.GetString(eDeposit.ToString(), R.Culture);
      return ret;
    }

    private static string Get(RunwayCondition.eDepth eDepth)
    {
      string ret;
      switch (eDepth)
      {
        case RunwayCondition.eDepth._40cmOrMore:
          ret = R.MoreThan40CM;
          break;
        case RunwayCondition.eDepth.lessThan1mm:
          ret = R.LessThan1MM;
          break;
        case RunwayCondition.eDepth.NotReportedOrClosed:
          ret = R.NotReportedOrClosed;
          break;
        case RunwayCondition.eDepth.Reserved:
          ret = R.Reserved;
          break;
        default:
          ret = eDepth.ToString().Substring(1);
          break;
      }

      return ret;
    }

    private static string Get(RunwayCondition.eContamination eContamination)
    {
      string ret = 
        R.ResourceManager.GetString(eContamination.ToString(), R.Culture);
      return ret;
    }
    #endregion Runway condition

    #region Windsheas

    private static string Get(WindShearInfo windShearInfo)
    {
      if (windShearInfo.IsAllRunways == false && windShearInfo.Count == 0)
        return "";

      if (windShearInfo.IsAllRunways)
        return R.WindshearAllRunways;
      else
      {
        StringBuilder ret = new StringBuilder();
        ret.Append(R.WindshearAt + R.Space);
        foreach (var fItem in windShearInfo)
          ret.Append(fItem.Runway + R.Comma);

        return ret.ToString();
      }
    }
    #endregion Windsheas

    #region Pressure

    private static string Get(PressureInfo pressureInfo)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.Pressure + R.Colon + R.Space);

      if (pressureInfo.Unit == PressureInfo.eUnit.hPa)
        ret.Append(pressureInfo.QNH + R.hPa);
      else
        ret.Append(pressureInfo.mmHq + R.mmHq);

      ret.Append(R.Dot);

      return ret.ToString();
    }

    #endregion Pressure

    #region Clouds

    private static string Get(CloudInfoWithNCD cloudInfoWithNCD)
    {
      StringBuilder ret = new StringBuilder();

      if (cloudInfoWithNCD.IsNCD)
        ret.Append(R.NoCloudsDetected);
      else
        ret = new StringBuilder( Get(cloudInfoWithNCD as CloudInfo));

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(CloudInfo cloudInfo)
    {
      StringBuilder ret = new StringBuilder();

      if (cloudInfo.IsNSC)
          ret.Append(R.NoSignificantCloud);
      else if (cloudInfo.IsSKC)
          ret.Append(R.SkyClear);
      else if (cloudInfo.IsCLR)
          ret.Append(R.SkyClear);
      else if (cloudInfo.IsVerticalVisibility)
      {
          if (cloudInfo.VVDistance.HasValue)
              ret.Append(R.VerticalVisibility + R.Space + cloudInfo.VVDistance.Value);
          else
              ret.Append(R.VerticalVisibility + R.Space + R.Unknown);
      }
      else
          foreach (var fItem in cloudInfo)
              ret.Append(Get(fItem) + R.Comma);

      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(Cloud fItem)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(Get(fItem.Type));
      ret.Append(R.Space);
      ret.Append(fItem.GetAltitudeIn(Common.eDistanceUnit.ft) + R.Ft + R.Space);
      if (fItem.IsCB)
        ret.Append(R.CB);
      if (fItem.IsTCU)
        ret.Append(R.TCU);

      return ret.ToString();
    }

    private static string Get(Cloud.eType eType)
    {
      return
        R.ResourceManager.GetString(eType.ToString(), R.Culture);
    }

    #endregion Clouds

    #region Phenomens

    private static string Get(PhenomInfo phenomInfo)
    {
      if (phenomInfo.Count == 0)
        return "";

      StringBuilder ret = new StringBuilder();

      ret.Append(R.Weather + R.Colon + R.Space);

      foreach (var fItem in phenomInfo)
        ret.Append(Get(fItem) + R.Semicolon + R.Space);

      ret.Append(R.Dot);

      return ret.ToString();

    }

    private static string Get(ePhenomCollection phenomList)
    {
      StringBuilder ret = new StringBuilder();

      foreach (var fItem in phenomList)
        ret.Append(Get(fItem) + R.Space);

      return ret.ToString();
    }

    private static string Get(ePhenomCollection.ePhenom fItem)
    {
      string ret = 
        R.ResourceManager.GetString(fItem.ToString(), R.Culture);
      return ret;
    }

    #endregion Phenomens

    #region Visibility

    private static string Get(VisibilityForMetar visibilityForMetar)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(Get(visibilityForMetar as Visibility));

      if (visibilityForMetar.IsClear == false)
      {
        if (visibilityForMetar.DirectionSpecification != null)
          ret.Append (R.VisibilityFrom + R.Space + Get(visibilityForMetar.DirectionSpecification.Value) + R.Space);

        if (visibilityForMetar.OtherDistance.HasValue)
          ret.Append(R.OpeningBracket + Get(visibilityForMetar.OtherDistance.Value) + R.Space + R.VisibilityFrom + R.Space +  Get(visibilityForMetar.OtherDirectionSpecification.Value) +  R.ClosingBracket + R.Space);

        if (visibilityForMetar.Runways.Count > 0)
        {
          string pom = Get(visibilityForMetar.Runways);
          ret.Append(R.Dot);
          ret.Append(pom);
        }
      }

        ret.Append(R.Dot);

        return ret.ToString();
    }

    private static string Get(List<RunwayVisibility> list)
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(R.RunwaysVisibilities + R.Colon + R.Space);
      foreach (var fItem in list)
        ret.Append(Get(fItem) + R.Semicolon + R.Space);
      
      ret.Append(R.Dot);

      return ret.ToString();
    }

    private static string Get(RunwayVisibility fItem)
    {
      StringBuilder ret = new StringBuilder (R.Runway + R.Space + fItem.Runway + R.Colon + R.Space);

      if (fItem.VariableDistance.HasValue)

          ret.Append(R.RunwayVisibilityVariableBetween + R.Space + Get(fItem.Distance) + R.Space + R.And + R.Space +  Get(fItem.VariableDistance.Value));
      else
          ret.Append(Get(fItem.Distance));

      if (fItem.Tendency.HasValue && fItem.Tendency.Value != RunwayVisibility.eTendency.N)
          ret.Append(R.Space + Get(fItem.Tendency.Value));

      return ret.ToString();
    }


    private static string Get(RunwayVisibility.eTendency tendency)
    {
        return R.ResourceManager.GetString("RunwayVisibilityTendency_" + tendency.ToString());
    }

    private static object Get(Types.Basic.NonNegInt nonNegInt)
    {
      return nonNegInt.ToString();
    }

    private static string Get(Common.eDirection eDirection)
    {
      string ret =
        R.ResourceManager.GetString(eDirection.ToString(), R.Culture);
      return ret;
    }

    private static string Get(Types.Basic.Racional racional)
    {
      StringBuilder ret = new StringBuilder();

      if (racional.Denominator == 1)
        ret.Append(racional.Value);
      else
        ret.Append(racional.Numerator + "/" + racional.Denominator);

      return ret.ToString();
    }

    #endregion Visibility

    #region Wind

    private static string Get(Types.WindWithVariability windWithVariability)
    {
      string ret = Get(windWithVariability as Wind);
      if (windWithVariability.IsVarying)
        ret += R.WindIsVaryingBetween + R.Space + windWithVariability.Variability.FromDirection.ToString("000") + R.Degree + R.Space +
          R.And + R.Space + windWithVariability.Variability.ToDirection.ToString("000") + R.Degree;

      ret += R.Dot;

      return ret;
    }

    private static string Get(Types.Common.eSpeedUnit eSpeedUnit)
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

    private static void AppendPreSpaced (StringBuilder sb, string data)
    {
      if (sb.Length > 0)
        sb.Append(" ");
      sb.Append(data);
    }
    private static string Get(Wind w)
    {
      StringBuilder ret = new StringBuilder();

      if (w.IsCalm)
        AppendPreSpaced(ret, R.WindCalm);
      else
      {
        ret.Append(R.Wind);

        ret.Append(R.Space);

        if (w.IsVariable)
          AppendPreSpaced(ret, R.WindVariable);
        else
          ret.Append(R.From + R.Space + w.Direction.Value.ToString("000") + R.Degree);

        ret.Append(R.Space);

        ret.Append(R.WindSpeedAt);

        ret.Append(R.Space);

        ret.Append(w.Speed.ToString());

        ret.Append(Get(w.Unit));

        if (w.GustSpeed.HasValue)
            ret.Append(R.Space + R.WindGustingTo + R.Space + w.GustSpeed.Value.ToString() + Get(w.Unit));

        ret.Append(R.Space);

      }

      ret.Append(R.Dot);

      return ret.ToString();
    }

    #endregion Wind

    private static string Get(Types.DateTimeTypes.DayHourMinute dayHourMinute)
    {
      // 1, 21:50Z
      string ret =
        dayHourMinute.Day + R.Comma + dayHourMinute.Hour.ToString("0") + R.Colon + dayHourMinute.Minute.ToString("00") + R.Zulu;

      return ret;
    }

    #region Reformat

    private static void Reformat (StringBuilder sb)
    {

      RemoveSpaces(sb);
      AdjustCasing(sb);
      AddSpaces(sb);

    }

    private static void AddSpaces(StringBuilder sb)
    {
      sb.Replace(".", ". ");
      sb.Replace(",", ", ");
    }

    private static void AdjustCasing(StringBuilder sb)
    {
      for (int i = 0; i < sb.Length - 1; i++)
      {
        if (sb[i] == '.')
          sb[i + 1] = char.ToUpper(sb[i + 1]);
      }
    }

    private static void DoReplace(StringBuilder sb, string original, string replacing)
    {
      while (sb.ToString().Contains(original))
        sb.Replace(original, replacing);
    }

    private static void RemoveSpaces(StringBuilder sb)
    {
      DoReplace(sb, " ;", ";");
      DoReplace(sb, ". ", ".");
      DoReplace(sb, " .", ".");
      DoReplace(sb, ", ", ",");
      DoReplace(sb, " ,", ",");
      DoReplace(sb, ",.", ".");
      DoReplace(sb, ";.", ".");
      DoReplace(sb, ",,", ",");      
      DoReplace(sb, "..", ".");
    }

    static char[] pncs = new char[] { '.', ',', ':' };
    private static bool IsPunctuation(char p)
    {
      if (pncs.Contains(p))
        return true;
      else
        return false;
    }
    #endregion Reformat


  }
}

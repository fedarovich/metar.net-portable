using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  
  /// <summary>
  /// Represents static class with common info
  /// </summary>
  public static class  Common
  {

    /// <summary>
    /// This enumeration is obsolete. Use <see cref="eDistanceUnit">eDistanceUnit</see>
    /// or <see cref="eSpeedUnit">eSpeedUnit</see> enumerations.
    /// </summary>
    [Obsolete("This type is obsolete and is no more used. Use eDistanceUnit or eSpeedUnit enumerations.", true)]
    public enum eUnit 
    { 
      /// <summary>
      /// Obsolete.
      /// </summary>
      ObsoleteUseDistanceUnitEnumeration
    }

    /// <summary>
    /// Distance units.
    /// </summary>
    public enum eDistanceUnit
    {
      /// <summary>
      /// Kilometres
      /// </summary>
      km = 0,
      /// <summary>
      /// Metres
      /// </summary>
      m = 1,
      /// <summary>
      /// Miles
      /// </summary>
      mi = 2,
      /// <summary>
      /// Feet
      /// </summary>
      ft = 3
    }

    private static double[,] distanceUnitConversionTable = new double[,] {
      {1, 1000, 0.621371192, 3280.8399},
      {0.001, 1, 0.000621371192, 3.2808399},
      {1.609344 , 1609.344, 1, 5280 },
      {0.0003048, 0.3048, 0.000189393939 ,1}
    };

    /// <summary>
    /// Speed units
    /// </summary>
    public enum eSpeedUnit
    {
      /// <summary>
      /// Knots
      /// </summary>
      kt = 0,
      /// <summary>
      /// Kilometers per hour
      /// </summary>
      kph = 1,
      /// <summary>
      /// Miles per hour
      /// </summary>
      mps = 2,
      /// <summary>
      /// Miles per hour
      /// </summary>
      miph = 3
    }

    private static double[,] speedUnitConversionTable = new double[,] {
      { 1, 1.85200, 0.5144444444444445, 1.1508},
      { 0.539956803, 1, 0.539956803, 0.6214},
      {0.539956803, 3.6, 1, 2.2369},
      {0.539956803, 0.539956803, 0.539956803, 1}
    };

    /// <summary>
    /// World directions
    /// </summary>
    public enum eDirection
    {
      /// <summary>
      /// North
      /// </summary>
      N,
      /// <summary>
      /// NorthEast
      /// </summary>
      NE,
      /// <summary>
      /// East
      /// </summary>
      E,
      /// <summary>
      /// SouthEast
      /// </summary>
      SE,
      /// <summary>
      /// South
      /// </summary>
      S,
      /// <summary>
      /// SouthWest
      /// </summary>
      SW,
      /// <summary>
      /// West
      /// </summary>
      W,
      /// <summary>
      /// NortWest
      /// </summary>
      NW
    }

    /// <summary>
    /// Represents state of the sea, as specified in WMO, CodeTable 3700
    /// </summary>
    public enum eSeaState
    {
      /// <summary>
      /// Calm (glassy)
      /// </summary>
      CalmGlassy = 0,
      /// <summary>
      /// Calm (rippled)
      /// </summary>
      CalmRippled = 1,
      /// <summary>
      /// Smooth
      /// </summary>
      Smooth=2,
      /// <summary>
      /// Slight
      /// </summary>
      Slight=3,
      /// <summary>
      /// Moderate
      /// </summary>
      Moderate=4,
      /// <summary>
      /// Rought
      /// </summary>
      Rought=5,
      /// <summary>
      /// Very rought
      /// </summary>
      VeryRough=6,
      /// <summary>
      /// High
      /// </summary>
      High=7,
      /// <summary>
      /// Very high
      /// </summary>
      VeryHigh=8,
      /// <summary>
      /// Phenomenal
      /// </summary>
      PhenomenalOver=9
    }

    /// <summary>
    /// Converts direction as integer into enum eDirection. <see cref="eDirection"/>
    /// </summary>
    /// <param name="heading">Heading, values from 0 to 360</param>
    /// <returns></returns>
    public static eDirection HeadingToeDirection(int heading)
    {
      if (!heading.IsBetween(0, 360))
        throw new ArgumentException("Invalid heading. Should be between 0 to 360.");

      if ((heading < 22) || (heading > 337))
        return  eDirection.N;
      else if (heading < 67)
        return eDirection.NE;
      else if (heading < 117)
        return eDirection.E;
      else if (heading < 157)
        return  eDirection.SE;
      else if (heading < 202)
        return eDirection.S;
      else if (heading < 247)
        return eDirection.SW;
      else if (heading < 292)
        return eDirection.W;
      else if (heading < 338)
        return eDirection.NW;
      else 
        throw new Exception("Invalid program state - unable recognize direction");
        
    }    

    /// <summary>
    /// Converts value from one distance unit to other.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="sourceUnit"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public static double Convert(double value, eDistanceUnit sourceUnit, eDistanceUnit targetUnit)
    {
      double ret = value * distanceUnitConversionTable[(int)sourceUnit, (int)targetUnit];

      return ret;
    }

    /// <summary>
    /// Converts value from one speed unit to other.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="sourceUnit"></param>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public static double Convert(double value, eSpeedUnit sourceUnit, eSpeedUnit targetUnit)
    {
      double ret = value * speedUnitConversionTable[(int)sourceUnit, (int)targetUnit];

      return ret;
    }
  }
}

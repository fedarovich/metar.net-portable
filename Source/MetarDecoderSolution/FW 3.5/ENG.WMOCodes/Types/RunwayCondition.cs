using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents info about runway conditions (contamination, depth and braking action).
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class RunwayCondition : ICodeItem
  {
    #region Nested

    /// <summary>
    /// Presents runway deposit.
    /// </summary>
    public enum eDeposit
    {
      /// <summary>
      /// Clean dry runway.
      /// </summary>
      CleanDry = 0,
      /// <summary>
      /// Wet runway.
      /// </summary>
      WetDamp = 1,
      /// <summary>
      /// Wet patches on runway.
      /// </summary>
      WetOrWetPatches = 2,
      /// <summary>
      /// Rime or frosts on runway.
      /// </summary>
      RimeOrFrost = 3,
      /// <summary>
      /// Dry snow on runay.
      /// </summary>
      DrySnow = 4,
      /// <summary>
      /// Wet snow on runway.
      /// </summary>
      WetSnow = 5,
      /// <summary>
      /// Slush on runway.
      /// </summary>
      Slush = 6,
      /// <summary>
      /// Ice on runway.
      /// </summary>
      Ice = 7,
      /// <summary>
      /// Compact snow on runway.
      /// </summary>
      CompactSnow = 8,
      /// <summary>
      /// Frozen ruts or ridges on runway.
      /// </summary>
      FrozentRutsRidges = 9
    }
    /// <summary>
    /// Represents amount of contamination on runway.
    /// </summary>
    public enum eContamination
    {
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved0 = 0,
      /// <summary>
      /// Less than 10%.
      /// </summary>
      LessThan10Percent = 1,
      /// <summary>
      /// More than 10% but less than 25%.
      /// </summary>
      LessThan25Percent = 2,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved3 = 3,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved4 = 4,
      /// <summary>
      /// More than 25%, but less than 50%.
      /// </summary>
      LessThan50Percent = 5,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved6 = 6,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved7 = 7,
      /// <summary>
      /// Reserved, not used.
      /// </summary>
      Reserved8 = 8,
      /// <summary>
      /// More than 50% including 100%.
      /// </summary>
      MoreThan50Percent = 9
    }
    /// <summary>
    /// Represents depth of contamination on the runway.
    /// </summary>
    public enum eDepth
    {
      ///<summary>
      ///lessThan1mm
      ///</summary>
      lessThan1mm = 0,
      ///<summary>
      ///_1mm
      ///</summary>
      _1mm = 1,
      ///<summary>
      ///_2mm
      ///</summary>
      _2mm = 2,
      ///<summary>
      ///_3mm
      ///</summary>
      _3mm = 3,
      ///<summary>
      ///_4mm
      ///</summary>
      _4mm = 4,
      ///<summary>
      ///_5mm
      ///</summary>
      _5mm = 5,
      ///<summary>
      ///_6mm
      ///</summary>
      _6mm = 6,
      ///<summary>
      ///_7mm
      ///</summary>
      _7mm = 7,
      ///<summary>
      ///_8mm
      ///</summary>
      _8mm = 8,
      ///<summary>
      ///_9mm
      ///</summary>
      _9mm = 9,
      ///<summary>
      ///_10mm
      ///</summary>
      _10mm = 10,
      ///<summary>
      ///_11mm
      ///</summary>
      _11mm = 11,
      ///<summary>
      ///_12mm
      ///</summary>
      _12mm = 12,
      ///<summary>
      ///_13mm
      ///</summary>
      _13mm = 13,
      ///<summary>
      ///_14mm
      ///</summary>
      _14mm = 14,
      ///<summary>
      ///_15mm
      ///</summary>
      _15mm = 15,
      ///<summary>
      ///_16mm
      ///</summary>
      _16mm = 16,
      ///<summary>
      ///_17mm
      ///</summary>
      _17mm = 17,
      ///<summary>
      ///_18mm
      ///</summary>
      _18mm = 18,
      ///<summary>
      ///_19mm
      ///</summary>
      _19mm = 19,
      ///<summary>
      ///_20mm
      ///</summary>
      _20mm = 20,
      ///<summary>
      ///_21mm
      ///</summary>
      _21mm = 21,
      ///<summary>
      ///_22mm
      ///</summary>
      _22mm = 22,
      ///<summary>
      ///_23mm
      ///</summary>
      _23mm = 23,
      ///<summary>
      ///_24mm
      ///</summary>
      _24mm = 24,
      ///<summary>
      ///_25mm
      ///</summary>
      _25mm = 25,
      ///<summary>
      ///_26mm
      ///</summary>
      _26mm = 26,
      ///<summary>
      ///_27mm
      ///</summary>
      _27mm = 27,
      ///<summary>
      ///_28mm
      ///</summary>
      _28mm = 28,
      ///<summary>
      ///_29mm
      ///</summary>
      _29mm = 29,
      ///<summary>
      ///_30mm
      ///</summary>
      _30mm = 30,
      ///<summary>
      ///_31mm
      ///</summary>
      _31mm = 31,
      ///<summary>
      ///_32mm
      ///</summary>
      _32mm = 32,
      ///<summary>
      ///_33mm
      ///</summary>
      _33mm = 33,
      ///<summary>
      ///_34mm
      ///</summary>
      _34mm = 34,
      ///<summary>
      ///_35mm
      ///</summary>
      _35mm = 35,
      ///<summary>
      ///_36mm
      ///</summary>
      _36mm = 36,
      ///<summary>
      ///_37mm
      ///</summary>
      _37mm = 37,
      ///<summary>
      ///_38mm
      ///</summary>
      _38mm = 38,
      ///<summary>
      ///_39mm
      ///</summary>
      _39mm = 39,
      ///<summary>
      ///_40mm
      ///</summary>
      _40mm = 40,
      ///<summary>
      ///_41mm
      ///</summary>
      _41mm = 41,
      ///<summary>
      ///_42mm
      ///</summary>
      _42mm = 42,
      ///<summary>
      ///_43mm
      ///</summary>
      _43mm = 43,
      ///<summary>
      ///_44mm
      ///</summary>
      _44mm = 44,
      ///<summary>
      ///_45mm
      ///</summary>
      _45mm = 45,
      ///<summary>
      ///_46mm
      ///</summary>
      _46mm = 46,
      ///<summary>
      ///_47mm
      ///</summary>
      _47mm = 47,
      ///<summary>
      ///_48mm
      ///</summary>
      _48mm = 48,
      ///<summary>
      ///_49mm
      ///</summary>
      _49mm = 49,
      ///<summary>
      ///_50mm
      ///</summary>
      _50mm = 50,
      ///<summary>
      ///_51mm
      ///</summary>
      _51mm = 51,
      ///<summary>
      ///_52mm
      ///</summary>
      _52mm = 52,
      ///<summary>
      ///_53mm
      ///</summary>
      _53mm = 53,
      ///<summary>
      ///_54mm
      ///</summary>
      _54mm = 54,
      ///<summary>
      ///_55mm
      ///</summary>
      _55mm = 55,
      ///<summary>
      ///_56mm
      ///</summary>
      _56mm = 56,
      ///<summary>
      ///_57mm
      ///</summary>
      _57mm = 57,
      ///<summary>
      ///_58mm
      ///</summary>
      _58mm = 58,
      ///<summary>
      ///_59mm
      ///</summary>
      _59mm = 59,
      ///<summary>
      ///_60mm
      ///</summary>
      _60mm = 60,
      ///<summary>
      ///_61mm
      ///</summary>
      _61mm = 61,
      ///<summary>
      ///_62mm
      ///</summary>
      _62mm = 62,
      ///<summary>
      ///_63mm
      ///</summary>
      _63mm = 63,
      ///<summary>
      ///_64mm
      ///</summary>
      _64mm = 64,
      ///<summary>
      ///_65mm
      ///</summary>
      _65mm = 65,
      ///<summary>
      ///_66mm
      ///</summary>
      _66mm = 66,
      ///<summary>
      ///_67mm
      ///</summary>
      _67mm = 67,
      ///<summary>
      ///_68mm
      ///</summary>
      _68mm = 68,
      ///<summary>
      ///_69mm
      ///</summary>
      _69mm = 69,
      ///<summary>
      ///_70mm
      ///</summary>
      _70mm = 70,
      ///<summary>
      ///_71mm
      ///</summary>
      _71mm = 71,
      ///<summary>
      ///_72mm
      ///</summary>
      _72mm = 72,
      ///<summary>
      ///_73mm
      ///</summary>
      _73mm = 73,
      ///<summary>
      ///_74mm
      ///</summary>
      _74mm = 74,
      ///<summary>
      ///_75mm
      ///</summary>
      _75mm = 75,
      ///<summary>
      ///_76mm
      ///</summary>
      _76mm = 76,
      ///<summary>
      ///_77mm
      ///</summary>
      _77mm = 77,
      ///<summary>
      ///_78mm
      ///</summary>
      _78mm = 78,
      ///<summary>
      ///_79mm
      ///</summary>
      _79mm = 79,
      ///<summary>
      ///_80mm
      ///</summary>
      _80mm = 80,
      ///<summary>
      ///_81mm
      ///</summary>
      _81mm = 81,
      ///<summary>
      ///_82mm
      ///</summary>
      _82mm = 82,
      ///<summary>
      ///_83mm
      ///</summary>
      _83mm = 83,
      ///<summary>
      ///_84mm
      ///</summary>
      _84mm = 84,
      ///<summary>
      ///_85mm
      ///</summary>
      _85mm = 85,
      ///<summary>
      ///_86mm
      ///</summary>
      _86mm = 86,
      ///<summary>
      ///_87mm
      ///</summary>
      _87mm = 87,
      ///<summary>
      ///_88mm
      ///</summary>
      _88mm = 88,
      ///<summary>
      ///_89mm
      ///</summary>
      _89mm = 89,
      ///<summary>
      ///_90mm
      ///</summary>
      _90mm = 90,
      ///<summary>
      ///Reserved
      ///</summary>
      Reserved = 91,
      ///<summary>
      ///_10cm
      ///</summary>
      _10cm = 92,
      ///<summary>
      ///_15cm
      ///</summary>
      _15cm = 93,
      ///<summary>
      ///_20cm
      ///</summary>
      _20cm = 94,
      ///<summary>
      ///_25cm
      ///</summary>
      _25cm = 95,
      ///<summary>
      ///_30cm
      ///</summary>
      _30cm = 96,
      ///<summary>
      ///_35cm
      ///</summary>
      _35cm = 97,
      ///<summary>
      ///_40cmOrMore
      ///</summary>
      _40cmOrMore = 98,
      ///<summary>
      ///NotReportedOrClosed
      ///</summary>
      NotReportedOrClosed = 99
    }
    /// <summary>
    /// Represents friction coefficient/braking action on runway
    /// </summary>
    public enum eFriction
    {
      ///<summary>
      ///Friction0_00
      ///</summary>
      Friction0_00 = 0,
      ///<summary>
      ///Friction0_01
      ///</summary>
      Friction0_01 = 1,
      ///<summary>
      ///Friction0_02
      ///</summary>
      Friction0_02 = 2,
      ///<summary>
      ///Friction0_03
      ///</summary>
      Friction0_03 = 3,
      ///<summary>
      ///Friction0_04
      ///</summary>
      Friction0_04 = 4,
      ///<summary>
      ///Friction0_05
      ///</summary>
      Friction0_05 = 5,
      ///<summary>
      ///Friction0_06
      ///</summary>
      Friction0_06 = 6,
      ///<summary>
      ///Friction0_07
      ///</summary>
      Friction0_07 = 7,
      ///<summary>
      ///Friction0_08
      ///</summary>
      Friction0_08 = 8,
      ///<summary>
      ///Friction0_09
      ///</summary>
      Friction0_09 = 9,
      ///<summary>
      ///Friction0_10
      ///</summary>
      Friction0_10 = 10,
      ///<summary>
      ///Friction0_11
      ///</summary>
      Friction0_11 = 11,
      ///<summary>
      ///Friction0_12
      ///</summary>
      Friction0_12 = 12,
      ///<summary>
      ///Friction0_13
      ///</summary>
      Friction0_13 = 13,
      ///<summary>
      ///Friction0_14
      ///</summary>
      Friction0_14 = 14,
      ///<summary>
      ///Friction0_15
      ///</summary>
      Friction0_15 = 15,
      ///<summary>
      ///Friction0_16
      ///</summary>
      Friction0_16 = 16,
      ///<summary>
      ///Friction0_17
      ///</summary>
      Friction0_17 = 17,
      ///<summary>
      ///Friction0_18
      ///</summary>
      Friction0_18 = 18,
      ///<summary>
      ///Friction0_19
      ///</summary>
      Friction0_19 = 19,
      ///<summary>
      ///Friction0_20
      ///</summary>
      Friction0_20 = 20,
      ///<summary>
      ///Friction0_21
      ///</summary>
      Friction0_21 = 21,
      ///<summary>
      ///Friction0_22
      ///</summary>
      Friction0_22 = 22,
      ///<summary>
      ///Friction0_23
      ///</summary>
      Friction0_23 = 23,
      ///<summary>
      ///Friction0_24
      ///</summary>
      Friction0_24 = 24,
      ///<summary>
      ///Friction0_25
      ///</summary>
      Friction0_25 = 25,
      ///<summary>
      ///Friction0_26
      ///</summary>
      Friction0_26 = 26,
      ///<summary>
      ///Friction0_27
      ///</summary>
      Friction0_27 = 27,
      ///<summary>
      ///Friction0_28
      ///</summary>
      Friction0_28 = 28,
      ///<summary>
      ///Friction0_29
      ///</summary>
      Friction0_29 = 29,
      ///<summary>
      ///Friction0_30
      ///</summary>
      Friction0_30 = 30,
      ///<summary>
      ///Friction0_31
      ///</summary>
      Friction0_31 = 31,
      ///<summary>
      ///Friction0_32
      ///</summary>
      Friction0_32 = 32,
      ///<summary>
      ///Friction0_33
      ///</summary>
      Friction0_33 = 33,
      ///<summary>
      ///Friction0_34
      ///</summary>
      Friction0_34 = 34,
      ///<summary>
      ///Friction0_35
      ///</summary>
      Friction0_35 = 35,
      ///<summary>
      ///Friction0_36
      ///</summary>
      Friction0_36 = 36,
      ///<summary>
      ///Friction0_37
      ///</summary>
      Friction0_37 = 37,
      ///<summary>
      ///Friction0_38
      ///</summary>
      Friction0_38 = 38,
      ///<summary>
      ///Friction0_39
      ///</summary>
      Friction0_39 = 39,
      ///<summary>
      ///Friction0_40
      ///</summary>
      Friction0_40 = 40,
      ///<summary>
      ///Friction0_41
      ///</summary>
      Friction0_41 = 41,
      ///<summary>
      ///Friction0_42
      ///</summary>
      Friction0_42 = 42,
      ///<summary>
      ///Friction0_43
      ///</summary>
      Friction0_43 = 43,
      ///<summary>
      ///Friction0_44
      ///</summary>
      Friction0_44 = 44,
      ///<summary>
      ///Friction0_45
      ///</summary>
      Friction0_45 = 45,
      ///<summary>
      ///Friction0_46
      ///</summary>
      Friction0_46 = 46,
      ///<summary>
      ///Friction0_47
      ///</summary>
      Friction0_47 = 47,
      ///<summary>
      ///Friction0_48
      ///</summary>
      Friction0_48 = 48,
      ///<summary>
      ///Friction0_49
      ///</summary>
      Friction0_49 = 49,
      ///<summary>
      ///Friction0_50
      ///</summary>
      Friction0_50 = 50,
      ///<summary>
      ///Friction0_51
      ///</summary>
      Friction0_51 = 51,
      ///<summary>
      ///Friction0_52
      ///</summary>
      Friction0_52 = 52,
      ///<summary>
      ///Friction0_53
      ///</summary>
      Friction0_53 = 53,
      ///<summary>
      ///Friction0_54
      ///</summary>
      Friction0_54 = 54,
      ///<summary>
      ///Friction0_55
      ///</summary>
      Friction0_55 = 55,
      ///<summary>
      ///Friction0_56
      ///</summary>
      Friction0_56 = 56,
      ///<summary>
      ///Friction0_57
      ///</summary>
      Friction0_57 = 57,
      ///<summary>
      ///Friction0_58
      ///</summary>
      Friction0_58 = 58,
      ///<summary>
      ///Friction0_59
      ///</summary>
      Friction0_59 = 59,
      ///<summary>
      ///Friction0_60
      ///</summary>
      Friction0_60 = 60,
      ///<summary>
      ///Friction0_61
      ///</summary>
      Friction0_61 = 61,
      ///<summary>
      ///Friction0_62
      ///</summary>
      Friction0_62 = 62,
      ///<summary>
      ///Friction0_63
      ///</summary>
      Friction0_63 = 63,
      ///<summary>
      ///Friction0_64
      ///</summary>
      Friction0_64 = 64,
      ///<summary>
      ///Friction0_65
      ///</summary>
      Friction0_65 = 65,
      ///<summary>
      ///Friction0_66
      ///</summary>
      Friction0_66 = 66,
      ///<summary>
      ///Friction0_67
      ///</summary>
      Friction0_67 = 67,
      ///<summary>
      ///Friction0_68
      ///</summary>
      Friction0_68 = 68,
      ///<summary>
      ///Friction0_69
      ///</summary>
      Friction0_69 = 69,
      ///<summary>
      ///Friction0_70
      ///</summary>
      Friction0_70 = 70,
      ///<summary>
      ///Friction0_71
      ///</summary>
      Friction0_71 = 71,
      ///<summary>
      ///Friction0_72
      ///</summary>
      Friction0_72 = 72,
      ///<summary>
      ///Friction0_73
      ///</summary>
      Friction0_73 = 73,
      ///<summary>
      ///Friction0_74
      ///</summary>
      Friction0_74 = 74,
      ///<summary>
      ///Friction0_75
      ///</summary>
      Friction0_75 = 75,
      ///<summary>
      ///Friction0_76
      ///</summary>
      Friction0_76 = 76,
      ///<summary>
      ///Friction0_77
      ///</summary>
      Friction0_77 = 77,
      ///<summary>
      ///Friction0_78
      ///</summary>
      Friction0_78 = 78,
      ///<summary>
      ///Friction0_79
      ///</summary>
      Friction0_79 = 79,
      ///<summary>
      ///Friction0_80
      ///</summary>
      Friction0_80 = 80,
      ///<summary>
      ///Friction0_81
      ///</summary>
      Friction0_81 = 81,
      ///<summary>
      ///Friction0_82
      ///</summary>
      Friction0_82 = 82,
      ///<summary>
      ///Friction0_83
      ///</summary>
      Friction0_83 = 83,
      ///<summary>
      ///Friction0_84
      ///</summary>
      Friction0_84 = 84,
      ///<summary>
      ///Friction0_85
      ///</summary>
      Friction0_85 = 85,
      ///<summary>
      ///Friction0_86
      ///</summary>
      Friction0_86 = 86,
      ///<summary>
      ///Friction0_87
      ///</summary>
      Friction0_87 = 87,
      ///<summary>
      ///Friction0_88
      ///</summary>
      Friction0_88 = 88,
      ///<summary>
      ///Friction0_89
      ///</summary>
      Friction0_89 = 89,
      ///<summary>
      ///Friction0_90
      ///</summary>
      Friction0_90 = 90,
      ///<summary>
      ///BrakingActionPoor
      ///</summary>
      BrakingActionPoor = 91,
      ///<summary>
      ///BrakingActionMediumPoor
      ///</summary>
      BrakingActionMediumPoor = 92,
      ///<summary>
      ///BrakingActionMedium
      ///</summary>
      BrakingActionMedium = 93,
      ///<summary>
      ///BrakingActionMediumGood
      ///</summary>
      BrakingActionMediumGood = 94,
      ///<summary>
      ///BrakingActionGood
      ///</summary>
      BrakingActionGood = 95,
      ///<summary>
      ///Reserved96
      ///</summary>
      Reserved96 = 96,
      ///<summary>
      ///Reserved97
      ///</summary>
      Reserved97 = 97,
      ///<summary>
      ///Reserved98
      ///</summary>
      Reserved98 = 98,
      ///<summary>
      ///BrakingActionUnreliable
      ///</summary>
      BrakingActionUnreliable = 99
    }
    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsCleared;
    ///<summary>
    /// Sets/gets if runway is cleared right now.
    ///</summary>
    public bool IsCleared
    {
      get
      {
        return (_IsCleared);
      }
      set
      {
        _IsCleared = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Runway;
    ///<summary>
    /// Sets/gets Runway name/sign.
    ///</summary>
    public string Runway
    {
      get
      {
        return (_Runway);
      }
      set
      {
        _Runway = value;
      }
    }

    /// <summary>
    /// True if definition is for all runways. Is true, if runway condition runway id is equal to 88.
    /// </summary>
    /// <value></value>
    public bool IsForAllRunways
    {
      get
      {
        return (Runway == "88");
      }
    }

    /// <summary>
    /// True if information is obsolete from last report. Is true, if runway condition runway id is equal to 99.
    /// </summary>
    public bool IsObsolete
    {
      get
      {
        return (Runway == "99");
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDeposit? _Deposit;
    ///<summary>
    /// Sets/gets deposit of runway. Null if unknown/not reported (that is / in metar).
    ///</summary>
    public eDeposit? Deposit
    {
      get
      {
        return (_Deposit);
      }
      set
      {
        _Deposit = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eContamination? _Contamination;
    ///<summary>
    /// Sets/gets contamination level of runway. Null if unknown/not reported (that is / in metar).
    ///</summary>
    public eContamination? Contamination
    {
      get
      {
        return (_Contamination);
      }
      set
      {
        _Contamination = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDepth? _Depth;
    ///<summary>
    /// Sets/gets contamination depth on runway. Null if unknown/not reported (that is // in metar).
    ///</summary>
    public eDepth? Depth
    {
      get
      {
        return (_Depth);
      }
      set
      {
        _Depth = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eFriction? _Friction;
    ///<summary>
    /// Sets/gets friction/braking effect of runway. Null if unknown/not reported (that is // in metar).
    ///</summary>
    public eFriction? Friction
    {
      get
      {
        return (_Friction);
      }
      set
      {
        _Friction = value;
      }
    }

    #endregion Properties

    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(
        "R" + Runway + "/");
      if (IsCleared)
        ret.Append("CLDR//");
      else
      {
        ret.Append(
          (Deposit.HasValue ? ((int)Deposit.Value).ToString() : "/"));
        ret.Append(
              (Contamination.HasValue ? ((int)Contamination.Value).ToString() : "/"));
        ret.Append(
        (Depth.HasValue ? ((int)Depth.Value).ToString("00") : "//"));
        ret.Append(
        (Friction.HasValue ? ((int)Friction.Value).ToString("00") : "//"));
      }

      return ret.ToString();
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
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (string.IsNullOrEmpty(Runway))
        errors.Add("Runway number/sign is not set.");

      if (Contamination.HasValue)
      {
        switch (Contamination.Value)
        {
          case eContamination.Reserved0:
          case eContamination.Reserved3:
          case eContamination.Reserved4:
          case eContamination.Reserved6:
          case eContamination.Reserved7:
          case eContamination.Reserved8:
            warnings.Add("This runway contamination value is reserved and should not be used.");
            break;
        }
      }
    }

    #endregion

    #endregion Inherited

  }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class RunwayVisibilityDecoder : TypeDecoder<RunwayVisibility>
  {
    public override string Description
    {
      get { return "Runway visibility"; }
    }

    public override string RegEx
    {
      get { return @"^(( ?R(\d{2}(R|L|C)?)/(M|P)?(\d{4})(V(\d{4}))?(FT|U|N|D)?))"; }
    }

    protected override RunwayVisibility _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      RunwayVisibility ret = new RunwayVisibility();

      ret.Runway = groups[3].Value;

      if (groups[5].Success)
        ret.DeviceMeasurementRestriction = (RunwayVisibility.eDeviceMeasurementRestriction)Enum.Parse(
          typeof(RunwayVisibility.eDeviceMeasurementRestriction), groups[5].Value, false);
      else
        ret.DeviceMeasurementRestriction = null;

      ret.Distance = groups[6].GetIntValue();

      if (groups[7].Success)
        ret.VariableDistance = groups[8].GetIntValue();

      if (groups[9].Success)
      {
        if (groups[9].Value == "FT")
        {
          ret.Unit = Common.eDistanceUnit.ft;
          ret.Tendency = null;
        }
        else
        {
          ret.Unit = Common.eDistanceUnit.m;
          ret.Tendency = (RunwayVisibility.eTendency)
            Enum.Parse(typeof(RunwayVisibility.eTendency), groups[9].Value, false);
        }
      }

      return ret;
    }
  }
}

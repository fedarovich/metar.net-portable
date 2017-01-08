using System;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    class RunwayVisibilityDecoder : TypeDecoder<RunwayVisibility>
    {
        public override string Description => "Runway visibility";

        public override string RegEx => @"^(( ?R(\d{2}(R|L|C)?)/(M|P)?(\d{4})(V(\d{4}))?(FT|U|N|D)?))";

        protected override RunwayVisibility _Decode(System.Text.RegularExpressions.GroupCollection groups)
        {
            RunwayVisibility ret = new RunwayVisibility {Runway = groups[3].Value};

            if (groups[5].Success)
                ret.DeviceMeasurementRestriction = (DeviceMeasurementRestriction)Enum.Parse(
                  typeof(DeviceMeasurementRestriction), groups[5].Value, false);
            else
                ret.DeviceMeasurementRestriction = null;

            ret.Distance = groups[6].GetIntValue();

            if (groups[7].Success)
                ret.VariableDistance = groups[8].GetIntValue();

            if (groups[9].Success)
            {
                if (groups[9].Value == "FT")
                {
                    ret.Unit = DistanceUnit.ft;
                    ret.Tendency = null;
                }
                else
                {
                    ret.Unit = DistanceUnit.m;
                    ret.Tendency = (RunwayVisibilityTendency)
                      Enum.Parse(typeof(RunwayVisibilityTendency), groups[9].Value, false);
                }
            }

            return ret;
        }
    }
}

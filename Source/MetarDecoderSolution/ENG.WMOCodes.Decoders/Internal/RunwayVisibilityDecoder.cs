using System;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class RunwayVisibilityDecoder : TypeDecoder<RunwayVisibility>
    {
        public override string Description => "Runway visibility";

        public override string RegEx => @"^(( ?R(?<runway>\d{2}(R|L|C)?)/(?<rest>M|P)?(?<dist>\d{4})(?<v>V(?<vrest>M|P)?(?<vdist>\d{4}))?(?<tend>FT|U|N|D)?))";

        protected override RunwayVisibility DecodeCore(GroupCollection groups)
        {
            RunwayVisibility ret = new RunwayVisibility { Runway = groups["runway"].Value };

            var distance = groups["dist"].GetIntValue();
            var restriction = groups["rest"].GetOptionalEnumValue<DeviceMeasurementRestriction>();
            ret.Distance = new RunwayVisibilityDistance(distance, restriction);

            if (groups["v"].Success)
            {
                var varDistance = groups["vdist"].GetIntValue();
                var varRestriction = groups["vrest"].GetOptionalEnumValue<DeviceMeasurementRestriction>();
                ret.VariableDistance = new RunwayVisibilityDistance(varDistance, varRestriction);
            }

            if (groups["tend"].Success)
            {
                if (groups["tend"].Value == "FT")
                {
                    ret.Unit = DistanceUnit.ft;
                    ret.Tendency = null;
                }
                else
                {
                    ret.Unit = DistanceUnit.m;
                    ret.Tendency = (RunwayVisibilityTendency)
                      Enum.Parse(typeof(RunwayVisibilityTendency), groups["tend"].Value, false);
                }
            }

            return ret;
        }
    }
}

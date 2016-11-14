using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class RunwayConditionInfoDecoder : CustomDecoder<RunwayConditionInfo>
  {
    private const string R_RWY_CONDS = @"(?#rwyCond)(( SNOCLO)|(( " + R_RWY_COND + ")*))?";
    private const string R_RWY_COND = @"R(\d{2}(L|R|C)*)/((\d|/)(\d|/)(\d{2}|/{2})(\d{2}|/{2})|(CLRD//))";

    private const string SNOCLO = "SNOCLO";

    protected override RunwayConditionInfo _Decode(ref string source)
    {
      RunwayConditionInfo ret = null;
      RunwayCondition rc;

      if (source.StartsWith(SNOCLO))
      {
        ret = new RunwayConditionInfo() { IsSNOCLO = true };
        source = source.Substring(SNOCLO.Length).TrimStart();
      }
      else
      {
        ret = new RunwayConditionInfo();
        bool found = true;
        while (found)
        {
          rc = new RunwayConditionDecoder() { Required = false }.Decode(ref source);
          if (rc == null)
            found = false;
          else
            ret.Add(rc);
        }
      }

      return ret;
    }

    public override string Description
    {
      get { return "Runways' conditions"; }
    }
  }
}

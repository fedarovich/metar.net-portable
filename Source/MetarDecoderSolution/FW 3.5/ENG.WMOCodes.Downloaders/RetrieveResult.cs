using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Downloaders
{
  /// <summary>
  /// Represents result of async code downloading.
  /// </summary>
  public class RetrieveResult
  {
    /// <summary>
    /// Contains exception if any raised during async code downloading.
    /// Null, if downloading was successfull.
    /// </summary>
    public Exception Exception { get; private set; }
    /// <summary>
    /// Contains downloaded code, if download was successfull. Undefined value otherwise.
    /// </summary>
    public string Result { get; private set; }

    /// <summary>
    /// Returns true if download was successfull. False if exception was raised.
    /// </summary>
    /// <remarks>
    /// If this property returns True, then correct downloaded code is available through Result property. 
    /// <seealso cref="Result"/>
    /// If this property returns False, then error occurs during async downloading. Exception associated with
    /// error is available through Exception property. <seealso cref="Exception"/>
    /// </remarks>
    public bool IsSuccessful
    {
      get
      {
        return Exception == null;
      }
    }

    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Downloader.MetarResult
    /// </summary>
    /// <param name="ex">Exception if async download was not successfull.</param>
    public RetrieveResult(Exception ex)
    {
      this.Exception = ex;
      this.Result = null;
    }
    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Downloader.MetarResult
    /// </summary>
    /// <param name="metar">Metar if async download was successfull.</param>
    public RetrieveResult(string metar)
    {
      this.Exception = null;
      this.Result = metar;
    }

  }
}

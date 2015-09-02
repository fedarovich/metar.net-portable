using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tutorial
{
  class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      FrmTest f = new FrmTest();
      f.Show();
      f.Focus();
      System.Windows.Forms.Application.Run(f);
      

      //DownloadMetarForHamburgSynchronically();

      //DownloadMetarForHamburgAsynchronically();

      //DecodeAndEncodeMetar();

      //PrintShortInfo();

      //PrintLongInfo();
    }

    private static void DecodeAndEncodeMetar()
    {
      // this is source example string
      string sourceMetar = "METAR LOWG 312320Z AUTO 00000KT 0200 R35/0650N R17/1200D BCFG 06/05 Q1010 RMK BASE S CLD004 N CLD007";

      // into this object decode metar will be stored
      ENG.WMOCodes.Codes.Metar metarObject = null;

      ENG.WMOCodes.Decoders.MetarDecoder decoder = new ENG.WMOCodes.Decoders.MetarDecoder();      

      try
      {
        // try to decode metar - call static Create method
        metarObject =
          decoder.Decode(sourceMetar);
      }
      catch (ENG.WMOCodes.Decoders.Internal.DecodeException ex)
      {
        // Error during decode
        Console.WriteLine("Unable to parse metar from string. "+ ex.Message);
      }
      catch (Exception ex)
      {
        // Other error
        Console.WriteLine("Unknown error during decode. Info: " + ex.Message);
      }

      // If successfully decoded
      if (metarObject != null)
      {
        // creates back metar string
        string targetMetar = metarObject.ToCode();

        // and compare string. should be the same.
        Console.WriteLine("Original metar:");
        Console.WriteLine(sourceMetar);
        Console.WriteLine("Decoded and encoded metar:");
        Console.WriteLine(targetMetar);
      }

      Console.ReadKey();
    }

    private static void DownloadMetarForHamburgAsynchronically()
    {
      Console.WriteLine("Downloading metar - asynchro...");

      // this specifies the downloader - from where and how the metar will be downloaded.
      ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever retriever =
        new ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever();

      ENG.WMOCodes.Downloaders.Downloader.DownloadAsync(
        "EDDH",
        retriever,
        new ENG.WMOCodes.Downloaders.Downloader.DownloadCompletedDelegate(OnCompleted));

      // do something other interesting stuff
    }

    private static void OnCompleted(ENG.WMOCodes.Downloaders.RetrieveResult result)
    {
      if (result.IsSuccessful)
      {
        Console.WriteLine("Metar for Hamburg is: ");
        Console.WriteLine(result.Result);
      }
      else
      {
        Console.WriteLine("Error occurs. Description: " + result.Exception.Message);
      }
    }

    private static void DownloadMetarForHamburgSynchronically()
    {
      Console.WriteLine("Downloading metar - synchro...");

      // here will be the result
       string eddhMetar;

      // this specifies the downloader - from where and how the metar will be downloaded.
       ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever retriever =
         new ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever();
      
      try
      {
        // synchronously download the metar, parameters are
        // 1) which airport; 2) from which source
        eddhMetar = ENG.WMOCodes.Downloaders.Downloader.Download("EDDH", retriever);

        Console.WriteLine("Metar for Hamburg is: ");
        Console.WriteLine(eddhMetar);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error occurs. Description: " + ex.Message);
      }

      Console.ReadKey();
    }

    private static void PrintShortInfo()
    {

      string sourceMetar = "METAR LOWG 312320Z AUTO 00000KT 0200 R35/0650N R17/1200D BCFG 06/05 Q1010 RMK BASE S CLD004 N CLD007";

      // decoding
      ENG.WMOCodes.Decoders.MetarDecoder metarDecoder = new ENG.WMOCodes.Decoders.MetarDecoder();
      ENG.WMOCodes.Codes.Metar metar = metarDecoder.Decode(sourceMetar);

      // formatting to short info with US culture
      ENG.WMOCodes.Formatters.ShortInfoFormatter.MetarFormatter metarFormatter =
        new ENG.WMOCodes.Formatters.ShortInfoFormatter.MetarFormatter();

      string str = metarFormatter.ToString(metar);

      Console.WriteLine(str);
      Console.ReadKey();
    }

    private static void PrintLongInfo()
    {
      string sourceMetar = "METAR LOWG 312320Z AUTO 00000KT 0200 R35/0650N R17/1200D BCFG 06/05 Q1010 RMK BASE S CLD004 N CLD007";
      ENG.WMOCodes.Codes.Metar m = null;
      ENG.WMOCodes.Decoders.MetarDecoder dec = new ENG.WMOCodes.Decoders.MetarDecoder();
      m = dec.Decode(sourceMetar);

      ENG.WMOCodes.Formatters.InfoFormatter.MetarFormatter mf = new ENG.WMOCodes.Formatters.InfoFormatter.MetarFormatter();

      string str = mf.ToString(m);

      Console.WriteLine(str);
      Console.ReadKey();
    }
  }
}

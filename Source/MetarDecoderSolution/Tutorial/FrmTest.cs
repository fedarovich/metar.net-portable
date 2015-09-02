using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tutorial
{
  public partial class FrmTest : Form
  {
    public FrmTest()
    {
      InitializeComponent();
    }

    private void btnSyncDown_Click(object sender, EventArgs e)
    {
      AddInfo("Starting sync download");

      string metar;
      string taf;

      // this specifies the downloader - from where and how the metar will be downloaded.
      ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever mRetriever = 
        new ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever();

      ENG.WMOCodes.Downloaders.Retrievers.Taf.NoaaGovRetriever tRetriever =
        new ENG.WMOCodes.Downloaders.Retrievers.Taf.NoaaGovRetriever();

      try
      {
        // synchronously download the metar, parameters are
        // 1) which airport; 2) from which source
        metar = ENG.WMOCodes.Downloaders.Downloader.Download(
          txtIcao.Text.Trim(), mRetriever);

        taf = ENG.WMOCodes.Downloaders.Downloader.Download(
          txtIcao.Text.Trim(), tRetriever);

        txtMetar.Text = metar;
        txtTaf.Text = taf;

        AddInfo("... downloaded");
      }
      catch (Exception ex)
      {
        AddInfo("Sync download failed - " + ex.GetMessages ());
      }
    }

    private void ClearInfo()
    {
      txtResult.Text = "";
    }
    private void AddInfo(string line)
    {
      txtResult.Text += line + "\r\n";

      txtResult.Select(txtResult.Text.Length, 0);
      txtResult.ScrollToCaret();
    }

    private void btnAsyncDown_Click(object sender, EventArgs e)
    {
      AddInfo("Downloading metar/taf - asynchro...");

      // this specifies the downloaders - from where and how the metar/taf will be downloaded.
      // starting with METAR first
      ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever retriever =
        new ENG.WMOCodes.Downloaders.Retrievers.Metar.NoaaGovRetriever();
      
      
      ENG.WMOCodes.Downloaders.Downloader.DownloadAsync(
          txtIcao.Text,
          retriever,
          new ENG.WMOCodes.Downloaders.Downloader.DownloadCompletedDelegate(OnMetarCompleted));

      // now get the TAF
      ENG.WMOCodes.Downloaders.Retrievers.Taf.NoaaGovRetriever tRetriever =
        new ENG.WMOCodes.Downloaders.Retrievers.Taf.NoaaGovRetriever();


      ENG.WMOCodes.Downloaders.Downloader.DownloadAsync(
          txtIcao.Text,
          tRetriever,
          new ENG.WMOCodes.Downloaders.Downloader.DownloadCompletedDelegate(OnTafCompleted));
        
        
        AddInfo("... asynchro request send, waiting for result.");



    }

    private void OnMetarCompleted(ENG.WMOCodes.Downloaders.RetrieveResult result)
    {
      if (this.InvokeRequired)
        this.Invoke (new Action (() => {OnMetarCompleted(result);}));
      else
      {
        if (result.IsSuccessful)
        {
          AddInfo("... asynchro metar result returned.");
          txtMetar.Text = result.Result;
        }
        else
        {
          AddInfo("Asynchro download failed - " + result.Exception.GetMessages());
        }
      }
    }

    private void OnTafCompleted(ENG.WMOCodes.Downloaders.RetrieveResult result)
    {
        if (this.InvokeRequired)
            this.Invoke(new Action(() => { OnTafCompleted(result); }));
        else
        {
            if (result.IsSuccessful)
            {
                AddInfo("... asynchro taf result returned.");
                txtTaf.Text = result.Result;
            }
            else
            {
                AddInfo("Asynchro download failed - " + result.Exception.GetMessages());
            }
        }
    }

    private void btnSanityCheck_Click(object sender, EventArgs e)
    {
      AddInfo("Performing sanity check...");

      ENG.WMOCodes.Codes.Metar mtr = GetMetar();

      if (mtr == null) return;

      List<string> er = new List<string>();
      List<string> w = new List<string>();

      mtr.SanityCheck(ref er, ref w);      

      er.ForEach(i => AddInfo("Sanity check error: " + i));
      w.ForEach(i => AddInfo("Sanity check warning: " + i));
      AddInfo("...done");
    }

    private ENG.WMOCodes.Codes.Metar GetMetar()
    {
        if (txtMetar.Text == "")
            return null;
        else
        {
            ENG.WMOCodes.Codes.Metar ret = null;
            ENG.WMOCodes.Decoders.MetarDecoder decoder = new ENG.WMOCodes.Decoders.MetarDecoder();
            //ENG.WMOCodes.Decoders.MetarDecoderWithAllOptional decoder =
            //  new ENG.WMOCodes.Decoders.MetarDecoderWithAllOptional();

            try
            {
                ret =
                  decoder.Decode(txtMetar.Text);
            }
            catch (Exception ex)
            {
                AddInfo("Error - " + ex.GetMessages());
            }
            return ret;
        }
    }

    private ENG.WMOCodes.Codes.Taf GetTaf()
    {
        if (txtTaf.Text == "")
            return null;
        else
        {
            ENG.WMOCodes.Codes.Taf ret = null;
            ENG.WMOCodes.Decoders.TafDecoder decoder = new ENG.WMOCodes.Decoders.TafDecoder();

            try
            {
                ret =
                  decoder.Decode(txtTaf.Text);
            }
            catch (Exception ex)
            {
                AddInfo("Error - " + ex.GetMessages());
            }

            return ret;
        }
    }

    private void btnTest_Click(object sender, EventArgs e)
    {
      MessageBox.Show("Not supported yet.");
      //AddInfo("Performing metar-string test...");

      //string er = null;

      //ENG.Metar.Decoder.Metar.CheckMetarString(txtMetar.Text, out er);

      //AddInfo("Found errors: " + er);
      //AddInfo("...done");
    }

    private void btnEncDec_Click(object sender, EventArgs e)
    {
      AddInfo("Performing encode-decode test...");

      ENG.WMOCodes.Codes.Metar mtr = GetMetar();

      if (mtr == null) return;

      AddInfo("Orig: \r\n" + txtMetar.Text.Trim());
      AddInfo("EnDe: \r\n" + mtr.ToCode());

      AddInfo("...done");
    }

    private void btnLongInfo_Click(object sender, EventArgs e)
    {
      AddInfo("Performing long-info print...");

      ENG.WMOCodes.Codes.Metar mtr = GetMetar();
      ENG.WMOCodes.Codes.Taf taf = GetTaf();

      if (mtr != null)
      {

      ENG.WMOCodes.Formatters.InfoFormatter.MetarFormatter formatter =
        new ENG.WMOCodes.Formatters.InfoFormatter.MetarFormatter();

      string str = formatter.ToString(mtr);

      AddInfo(" === METAR ===");
      AddInfo(str);
      }

      if (taf != null)
      {
      ENG.WMOCodes.Formatters.InfoFormatter.TafFormatter tafFormatter =
        new ENG.WMOCodes.Formatters.InfoFormatter.TafFormatter();

      string str = tafFormatter.ToString(taf);

      AddInfo(" === TAF ===");
      AddInfo(str);
      }

      AddInfo("...done");
    }

    private void btnShortInfo_Click(object sender, EventArgs e)
    {
      AddInfo("Performing short-info print...");

      ENG.WMOCodes.Codes.Metar mtr = GetMetar();

      if (mtr == null) return;

      ENG.WMOCodes.Formatters.ShortInfoFormatter.MetarFormatter formatter = 
        new ENG.WMOCodes.Formatters.ShortInfoFormatter.MetarFormatter();

      string str = formatter.ToString(mtr);

      AddInfo(str);

      AddInfo("...done");
    }
  }
}

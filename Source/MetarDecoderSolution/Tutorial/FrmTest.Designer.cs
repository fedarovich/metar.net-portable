namespace Tutorial
{
  partial class FrmTest
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.txtIcao = new System.Windows.Forms.TextBox();
      this.btnSyncDown = new System.Windows.Forms.Button();
      this.btnAsyncDown = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.txtMetar = new System.Windows.Forms.TextBox();
      this.btnSanityCheck = new System.Windows.Forms.Button();
      this.btnEncDec = new System.Windows.Forms.Button();
      this.btnShortInfo = new System.Windows.Forms.Button();
      this.btnLongInfo = new System.Windows.Forms.Button();
      this.txtResult = new System.Windows.Forms.TextBox();
      this.btnTest = new System.Windows.Forms.Button();
      this.txtTaf = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(43, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "ICAO:";
      // 
      // txtIcao
      // 
      this.txtIcao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.txtIcao.Location = new System.Drawing.Point(76, 10);
      this.txtIcao.MaxLength = 4;
      this.txtIcao.Name = "txtIcao";
      this.txtIcao.Size = new System.Drawing.Size(100, 21);
      this.txtIcao.TabIndex = 1;
      // 
      // btnSyncDown
      // 
      this.btnSyncDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSyncDown.Location = new System.Drawing.Point(410, 9);
      this.btnSyncDown.Name = "btnSyncDown";
      this.btnSyncDown.Size = new System.Drawing.Size(135, 23);
      this.btnSyncDown.TabIndex = 2;
      this.btnSyncDown.Text = "Sync download";
      this.btnSyncDown.UseVisualStyleBackColor = true;
      this.btnSyncDown.Click += new System.EventHandler(this.btnSyncDown_Click);
      // 
      // btnAsyncDown
      // 
      this.btnAsyncDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAsyncDown.Location = new System.Drawing.Point(551, 9);
      this.btnAsyncDown.Name = "btnAsyncDown";
      this.btnAsyncDown.Size = new System.Drawing.Size(135, 23);
      this.btnAsyncDown.TabIndex = 3;
      this.btnAsyncDown.Text = "Async download";
      this.btnAsyncDown.UseVisualStyleBackColor = true;
      this.btnAsyncDown.Click += new System.EventHandler(this.btnAsyncDown_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(14, 53);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(51, 15);
      this.label3.TabIndex = 4;
      this.label3.Text = "METAR:";
      // 
      // txtMetar
      // 
      this.txtMetar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMetar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.txtMetar.Location = new System.Drawing.Point(76, 50);
      this.txtMetar.Name = "txtMetar";
      this.txtMetar.Size = new System.Drawing.Size(610, 21);
      this.txtMetar.TabIndex = 5;
      this.txtMetar.Text = "METAR EETN 012150Z 26004G18KT 6000NE 0300S R04/0300V0500D R22/2000 +SNBR -RAHZ SC" +
    "T002 OVC012 04/04 Q1013 WS ALL RWY R08/000095 R26/CLRD// TEMPO AT1300 2000 BR RM" +
    "K OA2";
      // 
      // btnSanityCheck
      // 
      this.btnSanityCheck.Location = new System.Drawing.Point(180, 124);
      this.btnSanityCheck.Name = "btnSanityCheck";
      this.btnSanityCheck.Size = new System.Drawing.Size(131, 23);
      this.btnSanityCheck.TabIndex = 7;
      this.btnSanityCheck.Text = "Do sanity check";
      this.btnSanityCheck.UseVisualStyleBackColor = true;
      this.btnSanityCheck.Click += new System.EventHandler(this.btnSanityCheck_Click);
      // 
      // btnEncDec
      // 
      this.btnEncDec.Location = new System.Drawing.Point(317, 124);
      this.btnEncDec.Name = "btnEncDec";
      this.btnEncDec.Size = new System.Drawing.Size(179, 23);
      this.btnEncDec.TabIndex = 8;
      this.btnEncDec.Text = "Encode and decode";
      this.btnEncDec.UseVisualStyleBackColor = true;
      this.btnEncDec.Click += new System.EventHandler(this.btnEncDec_Click);
      // 
      // btnShortInfo
      // 
      this.btnShortInfo.Location = new System.Drawing.Point(502, 124);
      this.btnShortInfo.Name = "btnShortInfo";
      this.btnShortInfo.Size = new System.Drawing.Size(157, 23);
      this.btnShortInfo.TabIndex = 10;
      this.btnShortInfo.Text = "Print short info";
      this.btnShortInfo.UseVisualStyleBackColor = true;
      this.btnShortInfo.Click += new System.EventHandler(this.btnShortInfo_Click);
      // 
      // btnLongInfo
      // 
      this.btnLongInfo.Location = new System.Drawing.Point(502, 104);
      this.btnLongInfo.Name = "btnLongInfo";
      this.btnLongInfo.Size = new System.Drawing.Size(157, 23);
      this.btnLongInfo.TabIndex = 9;
      this.btnLongInfo.Text = "Print long info";
      this.btnLongInfo.UseVisualStyleBackColor = true;
      this.btnLongInfo.Click += new System.EventHandler(this.btnLongInfo_Click);
      // 
      // txtResult
      // 
      this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtResult.Location = new System.Drawing.Point(17, 153);
      this.txtResult.Multiline = true;
      this.txtResult.Name = "txtResult";
      this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtResult.Size = new System.Drawing.Size(669, 299);
      this.txtResult.TabIndex = 11;
      // 
      // btnTest
      // 
      this.btnTest.Location = new System.Drawing.Point(17, 124);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(131, 23);
      this.btnTest.TabIndex = 6;
      this.btnTest.Text = "Test metar string";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Visible = false;
      this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
      // 
      // txtTaf
      // 
      this.txtTaf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTaf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
      this.txtTaf.Location = new System.Drawing.Point(76, 77);
      this.txtTaf.Name = "txtTaf";
      this.txtTaf.Size = new System.Drawing.Size(610, 21);
      this.txtTaf.TabIndex = 13;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(14, 80);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(33, 15);
      this.label2.TabIndex = 12;
      this.label2.Text = "TAF:";
      // 
      // FrmTest
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(717, 464);
      this.Controls.Add(this.txtTaf);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnTest);
      this.Controls.Add(this.txtResult);
      this.Controls.Add(this.btnLongInfo);
      this.Controls.Add(this.btnShortInfo);
      this.Controls.Add(this.btnEncDec);
      this.Controls.Add(this.btnSanityCheck);
      this.Controls.Add(this.txtMetar);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnAsyncDown);
      this.Controls.Add(this.btnSyncDown);
      this.Controls.Add(this.txtIcao);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.Name = "FrmTest";
      this.Text = "FrmTest";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtIcao;
    private System.Windows.Forms.Button btnSyncDown;
    private System.Windows.Forms.Button btnAsyncDown;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtMetar;
    private System.Windows.Forms.Button btnSanityCheck;
    private System.Windows.Forms.Button btnEncDec;
    private System.Windows.Forms.Button btnShortInfo;
    private System.Windows.Forms.Button btnLongInfo;
    private System.Windows.Forms.TextBox txtResult;
    private System.Windows.Forms.Button btnTest;
    private System.Windows.Forms.TextBox txtTaf;
    private System.Windows.Forms.Label label2;
  }
}
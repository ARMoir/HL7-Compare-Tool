namespace HL7_Compare_Tool
{
    partial class HL7CompareTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HL7CompareTool));
            this.LoadOriginal = new System.Windows.Forms.Button();
            this.LoadCompare = new System.Windows.Forms.Button();
            this.OriginalPath = new System.Windows.Forms.TextBox();
            this.ComparePath = new System.Windows.Forms.TextBox();
            this.Report = new System.Windows.Forms.Button();
            this.ReportWindow = new System.Windows.Forms.WebBrowser();
            this.MainStrip = new System.Windows.Forms.StatusStrip();
            this.Export = new System.Windows.Forms.Button();
            this.UserLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UserTXT = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainProBar = new System.Windows.Forms.ToolStripProgressBar();
            this.MainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadOriginal
            // 
            this.LoadOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadOriginal.BackgroundImage = global::HL7_Compare_Tool.Properties.Resources.MoneyTwins;
            this.LoadOriginal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LoadOriginal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LoadOriginal.Image = global::HL7_Compare_Tool.Properties.Resources.Last;
            this.LoadOriginal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoadOriginal.Location = new System.Drawing.Point(571, 29);
            this.LoadOriginal.Name = "LoadOriginal";
            this.LoadOriginal.Size = new System.Drawing.Size(194, 30);
            this.LoadOriginal.TabIndex = 0;
            this.LoadOriginal.Text = "Original HL7";
            this.LoadOriginal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LoadOriginal.UseVisualStyleBackColor = true;
            this.LoadOriginal.Click += new System.EventHandler(this.LoadOriginal_Click);
            // 
            // LoadCompare
            // 
            this.LoadCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadCompare.BackgroundImage = global::HL7_Compare_Tool.Properties.Resources.MoneyTwins;
            this.LoadCompare.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LoadCompare.ForeColor = System.Drawing.Color.MidnightBlue;
            this.LoadCompare.Image = global::HL7_Compare_Tool.Properties.Resources.Last;
            this.LoadCompare.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LoadCompare.Location = new System.Drawing.Point(571, 68);
            this.LoadCompare.Name = "LoadCompare";
            this.LoadCompare.Size = new System.Drawing.Size(194, 30);
            this.LoadCompare.TabIndex = 1;
            this.LoadCompare.Text = "Compare HL7";
            this.LoadCompare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LoadCompare.UseVisualStyleBackColor = true;
            this.LoadCompare.Click += new System.EventHandler(this.LoadCompare_Click);
            // 
            // OriginalPath
            // 
            this.OriginalPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OriginalPath.Location = new System.Drawing.Point(12, 33);
            this.OriginalPath.Name = "OriginalPath";
            this.OriginalPath.Size = new System.Drawing.Size(553, 22);
            this.OriginalPath.TabIndex = 2;
            // 
            // ComparePath
            // 
            this.ComparePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComparePath.Location = new System.Drawing.Point(12, 72);
            this.ComparePath.Name = "ComparePath";
            this.ComparePath.Size = new System.Drawing.Size(553, 22);
            this.ComparePath.TabIndex = 3;
            // 
            // Report
            // 
            this.Report.BackgroundImage = global::HL7_Compare_Tool.Properties.Resources.MoneyTwins;
            this.Report.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Report.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Report.Image = global::HL7_Compare_Tool.Properties.Resources.Import;
            this.Report.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Report.Location = new System.Drawing.Point(12, 100);
            this.Report.Name = "Report";
            this.Report.Size = new System.Drawing.Size(168, 50);
            this.Report.TabIndex = 4;
            this.Report.Text = "Report";
            this.Report.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Report.UseVisualStyleBackColor = true;
            this.Report.Click += new System.EventHandler(this.Report_Click);
            // 
            // ReportWindow
            // 
            this.ReportWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportWindow.Location = new System.Drawing.Point(12, 156);
            this.ReportWindow.MinimumSize = new System.Drawing.Size(20, 20);
            this.ReportWindow.Name = "ReportWindow";
            this.ReportWindow.Size = new System.Drawing.Size(753, 277);
            this.ReportWindow.TabIndex = 5;
            // 
            // MainStrip
            // 
            this.MainStrip.BackgroundImage = global::HL7_Compare_Tool.Properties.Resources.MoneyTwins;
            this.MainStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MainStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserLabel,
            this.UserTXT,
            this.ProgressLabel,
            this.MainProBar});
            this.MainStrip.Location = new System.Drawing.Point(0, 433);
            this.MainStrip.Name = "MainStrip";
            this.MainStrip.Size = new System.Drawing.Size(793, 25);
            this.MainStrip.TabIndex = 6;
            this.MainStrip.Text = "MainStrip";
            // 
            // Export
            // 
            this.Export.BackgroundImage = global::HL7_Compare_Tool.Properties.Resources.MoneyTwins;
            this.Export.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Export.Image = global::HL7_Compare_Tool.Properties.Resources.Save;
            this.Export.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Export.Location = new System.Drawing.Point(186, 99);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(168, 51);
            this.Export.TabIndex = 7;
            this.Export.Text = "Export";
            this.Export.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(41, 20);
            this.UserLabel.Text = "User:";
            // 
            // UserTXT
            // 
            this.UserTXT.ForeColor = System.Drawing.Color.MidnightBlue;
            this.UserTXT.Name = "UserTXT";
            this.UserTXT.Size = new System.Drawing.Size(97, 20);
            this.UserTXT.Text = "Domain/User";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(68, 20);
            this.ProgressLabel.Text = "Progress:";
            // 
            // MainProBar
            // 
            this.MainProBar.Name = "MainProBar";
            this.MainProBar.Size = new System.Drawing.Size(100, 19);
            // 
            // HL7CompareTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HL7_Compare_Tool.Properties.Resources.MoneyTwins;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(793, 458);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.MainStrip);
            this.Controls.Add(this.ReportWindow);
            this.Controls.Add(this.Report);
            this.Controls.Add(this.ComparePath);
            this.Controls.Add(this.OriginalPath);
            this.Controls.Add(this.LoadCompare);
            this.Controls.Add(this.LoadOriginal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HL7CompareTool";
            this.Text = "HL7 Compare Tool";
            this.MainStrip.ResumeLayout(false);
            this.MainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadOriginal;
        private System.Windows.Forms.Button LoadCompare;
        private System.Windows.Forms.TextBox OriginalPath;
        private System.Windows.Forms.TextBox ComparePath;
        private System.Windows.Forms.Button Report;
        private System.Windows.Forms.WebBrowser ReportWindow;
        private System.Windows.Forms.StatusStrip MainStrip;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.ToolStripStatusLabel UserLabel;
        private System.Windows.Forms.ToolStripStatusLabel UserTXT;
        private System.Windows.Forms.ToolStripStatusLabel ProgressLabel;
        private System.Windows.Forms.ToolStripProgressBar MainProBar;
    }
}


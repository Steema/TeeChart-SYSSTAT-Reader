namespace SYSSTATS_Charter
{
    partial class SYSSTAT_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbChart = new System.Windows.Forms.GroupBox();
            this.bExport = new System.Windows.Forms.Button();
            this.gbReports = new System.Windows.Forms.GroupBox();
            this.cbMouseOver = new System.Windows.Forms.CheckBox();
            this.cbUseBarSeries = new System.Windows.Forms.CheckBox();
            this.cbNonZero = new System.Windows.Forms.CheckBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.lFilter = new System.Windows.Forms.Label();
            this.cbReportTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bOpenDir = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cbUTC = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbChart.SuspendLayout();
            this.gbReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbChart);
            this.splitContainer1.Panel1.Controls.Add(this.gbReports);
            this.splitContainer1.Panel1.Controls.Add(this.bOpenDir);
            this.splitContainer1.Size = new System.Drawing.Size(1205, 665);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // gbChart
            // 
            this.gbChart.Controls.Add(this.cbUTC);
            this.gbChart.Controls.Add(this.bExport);
            this.gbChart.Enabled = false;
            this.gbChart.Location = new System.Drawing.Point(25, 241);
            this.gbChart.Name = "gbChart";
            this.gbChart.Size = new System.Drawing.Size(358, 100);
            this.gbChart.TabIndex = 0;
            this.gbChart.TabStop = false;
            this.gbChart.Text = "Chart";
            // 
            // bExport
            // 
            this.bExport.Location = new System.Drawing.Point(217, 60);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(131, 23);
            this.bExport.TabIndex = 1;
            this.bExport.Text = "Export Chart to jpg ...";
            this.bExport.UseVisualStyleBackColor = true;
            this.bExport.Click += new System.EventHandler(this.BExport_Click);
            // 
            // gbReports
            // 
            this.gbReports.Controls.Add(this.cbMouseOver);
            this.gbReports.Controls.Add(this.cbUseBarSeries);
            this.gbReports.Controls.Add(this.cbNonZero);
            this.gbReports.Controls.Add(this.cbFilter);
            this.gbReports.Controls.Add(this.lFilter);
            this.gbReports.Controls.Add(this.cbReportTypes);
            this.gbReports.Controls.Add(this.label1);
            this.gbReports.Enabled = false;
            this.gbReports.Location = new System.Drawing.Point(25, 65);
            this.gbReports.Name = "gbReports";
            this.gbReports.Size = new System.Drawing.Size(358, 158);
            this.gbReports.TabIndex = 0;
            this.gbReports.TabStop = false;
            this.gbReports.Text = "Reports";
            // 
            // cbMouseOver
            // 
            this.cbMouseOver.AutoSize = true;
            this.cbMouseOver.Checked = true;
            this.cbMouseOver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMouseOver.Location = new System.Drawing.Point(17, 120);
            this.cbMouseOver.Name = "cbMouseOver";
            this.cbMouseOver.Size = new System.Drawing.Size(255, 19);
            this.cbMouseOver.TabIndex = 6;
            this.cbMouseOver.Text = "Mouseover Legend items to see description";
            this.cbMouseOver.UseVisualStyleBackColor = true;
            // 
            // cbUseBarSeries
            // 
            this.cbUseBarSeries.AutoSize = true;
            this.cbUseBarSeries.Location = new System.Drawing.Point(65, 95);
            this.cbUseBarSeries.Name = "cbUseBarSeries";
            this.cbUseBarSeries.Size = new System.Drawing.Size(98, 19);
            this.cbUseBarSeries.TabIndex = 4;
            this.cbUseBarSeries.Text = "Use Bar Series";
            this.cbUseBarSeries.UseVisualStyleBackColor = true;
            this.cbUseBarSeries.CheckedChanged += new System.EventHandler(this.CbUseBarSeries_CheckedChanged);
            // 
            // cbNonZero
            // 
            this.cbNonZero.AutoSize = true;
            this.cbNonZero.Checked = true;
            this.cbNonZero.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNonZero.Location = new System.Drawing.Point(65, 70);
            this.cbNonZero.Name = "cbNonZero";
            this.cbNonZero.Size = new System.Drawing.Size(106, 19);
            this.cbNonZero.TabIndex = 3;
            this.cbNonZero.Text = "Only Non-Zero";
            this.cbNonZero.UseVisualStyleBackColor = true;
            this.cbNonZero.CheckedChanged += new System.EventHandler(this.CbNonZero_CheckedChanged);
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.Enabled = false;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(227, 68);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(121, 23);
            this.cbFilter.TabIndex = 2;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.CbFilter_SelectedIndexChanged);
            // 
            // lFilter
            // 
            this.lFilter.AutoSize = true;
            this.lFilter.Enabled = false;
            this.lFilter.Location = new System.Drawing.Point(183, 71);
            this.lFilter.Name = "lFilter";
            this.lFilter.Size = new System.Drawing.Size(34, 15);
            this.lFilter.TabIndex = 1;
            this.lFilter.Text = "filter:";
            // 
            // cbReportTypes
            // 
            this.cbReportTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportTypes.FormattingEnabled = true;
            this.cbReportTypes.Location = new System.Drawing.Point(65, 31);
            this.cbReportTypes.Name = "cbReportTypes";
            this.cbReportTypes.Size = new System.Drawing.Size(156, 23);
            this.cbReportTypes.TabIndex = 0;
            this.cbReportTypes.SelectedIndexChanged += new System.EventHandler(this.CbReportTypes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Types:";
            // 
            // bOpenDir
            // 
            this.bOpenDir.Location = new System.Drawing.Point(25, 22);
            this.bOpenDir.Name = "bOpenDir";
            this.bOpenDir.Size = new System.Drawing.Size(186, 23);
            this.bOpenDir.TabIndex = 0;
            this.bOpenDir.Text = "Open Directory ...";
            this.bOpenDir.UseVisualStyleBackColor = true;
            this.bOpenDir.Click += new System.EventHandler(this.BOpenDir_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "jpg files|*.jpg";
            // 
            // cbUTC
            // 
            this.cbUTC.AutoSize = true;
            this.cbUTC.Location = new System.Drawing.Point(17, 32);
            this.cbUTC.Name = "cbUTC";
            this.cbUTC.Size = new System.Drawing.Size(100, 19);
            this.cbUTC.TabIndex = 2;
            this.cbUTC.Text = "UTC DateTime";
            this.cbUTC.UseVisualStyleBackColor = true;
            this.cbUTC.CheckedChanged += new System.EventHandler(this.CbUTC_CheckedChanged);
            // 
            // SYSSTAT_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 665);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SYSSTAT_Form";
            this.Text = "SYSSTAT-Charter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbChart.ResumeLayout(false);
            this.gbChart.PerformLayout();
            this.gbReports.ResumeLayout(false);
            this.gbReports.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button bOpenDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox gbReports;
        private System.Windows.Forms.ComboBox cbReportTypes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label lFilter;
        private System.Windows.Forms.CheckBox cbNonZero;
        private System.Windows.Forms.CheckBox cbUseBarSeries;
        private System.Windows.Forms.CheckBox cbMouseOver;
        private System.Windows.Forms.Button bExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox gbChart;
        private System.Windows.Forms.CheckBox cbUTC;
    }
}


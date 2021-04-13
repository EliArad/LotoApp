
namespace LotoApp
{
    partial class Form2
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
            this.chartViewer1 = new ChartDirector.WinChartViewer();
            this.btnShowDiffOnly = new System.Windows.Forms.Button();
            this.txtDiffToShow = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartViewer1
            // 
            this.chartViewer1.HotSpotCursor = System.Windows.Forms.Cursors.Hand;
            this.chartViewer1.Location = new System.Drawing.Point(3, 0);
            this.chartViewer1.Name = "chartViewer1";
            this.chartViewer1.Size = new System.Drawing.Size(1184, 559);
            this.chartViewer1.TabIndex = 1;
            this.chartViewer1.TabStop = false;
            // 
            // btnShowDiffOnly
            // 
            this.btnShowDiffOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowDiffOnly.Location = new System.Drawing.Point(133, 574);
            this.btnShowDiffOnly.Name = "btnShowDiffOnly";
            this.btnShowDiffOnly.Size = new System.Drawing.Size(75, 23);
            this.btnShowDiffOnly.TabIndex = 2;
            this.btnShowDiffOnly.Text = "Show";
            this.btnShowDiffOnly.UseVisualStyleBackColor = true;
            this.btnShowDiffOnly.Click += new System.EventHandler(this.btnShowDiffOnly_Click);
            // 
            // txtDiffToShow
            // 
            this.txtDiffToShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDiffToShow.Location = new System.Drawing.Point(27, 576);
            this.txtDiffToShow.Name = "txtDiffToShow";
            this.txtDiffToShow.Size = new System.Drawing.Size(100, 20);
            this.txtDiffToShow.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1185, 624);
            this.Controls.Add(this.txtDiffToShow);
            this.Controls.Add(this.btnShowDiffOnly);
            this.Controls.Add(this.chartViewer1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartViewer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ChartDirector.WinChartViewer chartViewer1;
        private System.Windows.Forms.Button btnShowDiffOnly;
        private System.Windows.Forms.TextBox txtDiffToShow;
    }
}
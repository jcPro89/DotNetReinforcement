namespace TareksAccount.Presentation
{
    partial class frmFullExchangeRateHistory
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
            this.dtgFullExchangeRateHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFullExchangeRateHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgFullExchangeRateHistory
            // 
            this.dtgFullExchangeRateHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgFullExchangeRateHistory.Location = new System.Drawing.Point(12, 147);
            this.dtgFullExchangeRateHistory.Name = "dtgFullExchangeRateHistory";
            this.dtgFullExchangeRateHistory.Size = new System.Drawing.Size(776, 291);
            this.dtgFullExchangeRateHistory.TabIndex = 0;
            // 
            // frmFullExchangeRateHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtgFullExchangeRateHistory);
            this.Name = "frmFullExchangeRateHistory";
            this.Text = "frmFullExchangeRateHistory";
            this.Load += new System.EventHandler(this.frmFullExchangeRateHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgFullExchangeRateHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgFullExchangeRateHistory;
    }
}
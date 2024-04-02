namespace TareksAccount.Presentation
{
    partial class frmCustomMessageBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomMessageBox));
            this.lblMessage = new LollipopLabel();
            this.imlIcon = new System.Windows.Forms.ImageList(this.components);
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.btnOK = new LollipopButton();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.lblMessage.Location = new System.Drawing.Point(109, 65);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(218, 96);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Operation succeed!";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imlIcon
            // 
            this.imlIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcon.ImageStream")));
            this.imlIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcon.Images.SetKeyName(0, "okSuccessIcon.png");
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(12, 65);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(96, 96);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 5;
            this.picIcon.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BGColor = "#508ef5";
            this.btnOK.FontColor = "#ffffff";
            this.btnOK.Location = new System.Drawing.Point(112, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(143, 41);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmCustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 221);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblMessage);
            this.MaximizeBox = false;
            this.Name = "frmCustomMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private LollipopLabel lblMessage;
        private System.Windows.Forms.ImageList imlIcon;
        private System.Windows.Forms.PictureBox picIcon;
        private LollipopButton btnOK;
    }
}
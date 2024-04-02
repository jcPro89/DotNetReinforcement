using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace TareksAccount.Presentation
{
    public partial class frmCustomMessageBox : MaterialForm
    {
        public frmCustomMessageBox(string message, MessageType type)
        {
            InitializeComponent();


            lblMessage.Text = message;
            //groupBox1.BackColor = MaterialSkin.Primary.Blue500;
            switch (type)
            {
                case MessageType.success:
                    picIcon.Image = imlIcon.Images["okSuccessIcon.png"];
                    break;
                case MessageType.information:
                    break;
                case MessageType.error:
                    break;
                case MessageType.warning:
                    break;
            }
        }

        public enum MessageType
        {
            success, warning, error, information
        }

        public static void ShowCustomMessage(string message, MessageType type)
        {
            
            new frmCustomMessageBox(message, type).ShowDialog();
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

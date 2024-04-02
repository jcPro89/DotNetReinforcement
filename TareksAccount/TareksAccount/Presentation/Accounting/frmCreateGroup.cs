using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TareksAccount.Presentation.Accounting
{
    public partial class frmCreateGroup : MetroForm
    {
        public frmCreateGroup()
        {
            InitializeComponent();
        }

        private void frmCreateGroup_Load(object sender, EventArgs e)
        {
            // LOAD ACCOUNT TYPES

            cbxAccountTypes.DataSource = Logic.Accounting.GroupLogic.LoadAccountTypes();
            cbxAccountTypes.DisplayMember = "Name";
            cbxAccountTypes.ValueMember = "Id";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int oAffected = Logic.Accounting.GroupLogic.CreateLedgerGroup(txtCode.Text, txtName.Text, txtDescription.Text, Convert.ToInt32(cbxAccountTypes.SelectedValue), chkAllowSubAccounts.Checked, chkPL_Account.Checked, !chkIsActive.Checked, frmLogin.iSelectedCompanyId);
                    if(oAffected==1)
                {
                    //MessageBox.Show("Ledger group " + txtGroupName.Text + " added succesfully");
                    frmCustomMessageBox.ShowCustomMessage("Ledger group " + txtName.Text + " added succesfully", frmCustomMessageBox.MessageType.success);
                }
                    else
                {
                    MessageBox.Show("There was a problem while creating the group.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem while creating the group: " + ex.Message);
            }

        }
    }
}

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
    public partial class frmCreateSubGroup : MetroForm
    {
        public frmCreateSubGroup()
        {
            InitializeComponent();
        }

        private void frmCreateSubGroup_Load(object sender, EventArgs e)
        {
            //LOAD GROUPS IN COMBOBOX

            cbxGroup.DataSource = Logic.Accounting.GroupLogic.LoadAllLedgerGroups(frmLogin.iSelectedCompanyId);
            cbxGroup.DisplayMember = "Name";
            cbxGroup.ValueMember = "Id";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //SAVE SUB GROUP TO DATABASE

                int oAffected = Logic.Accounting.SubGroupLogic.CreateLedgerSubGroup(txtCode.Text, txtName.Text, Convert.ToInt32(cbxGroup.SelectedValue), txtDescription.Text, !chkFreeze.Checked);
                if (oAffected == 1)
                {
                    frmCustomMessageBox.ShowCustomMessage("Sub Group '" + txtName.Text + "' added succesfully", frmCustomMessageBox.MessageType.success);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding Sub Group. Error details: " + ex.Message);
            }
        }
    }
}

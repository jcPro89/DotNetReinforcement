using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TareksAccount.Presentation.Accounting
{
    public partial class frmAccountAdd : MetroForm
    {
        public frmAccountAdd()
        {
            InitializeComponent();
        }

        bool bDataIsLoaded=false;

        private void frmAccountAdd_Load(object sender, EventArgs e)
        {

            //LOAD GROUPS
            cbxGroup.DataSource = Logic.Accounting.GroupLogic.LoadAllLedgerGroups(frmLogin.iSelectedCompanyId);
            cbxGroup.DisplayMember = "Name";
            cbxGroup.ValueMember = "Id";

            bDataIsLoaded = true;
        }

        private void cbxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bDataIsLoaded)
            {
                //LOAD GROUP'S SUBGROUPS
                cbxSubgroup.DataSource = Logic.Accounting.SubGroupLogic.SearchSubGroupsByGroupId(Convert.ToInt32(cbxGroup.SelectedValue));
                cbxSubgroup.DisplayMember = "Name";
                cbxSubgroup.ValueMember = "Id";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int oAffected = Logic.Accounting.AccountsLogic.AddAccount(Convert.ToInt32(cbxSubgroup.SelectedValue), txtName.Text, txtDescription.Text);
                if (oAffected == 1)
                {
                    frmCustomMessageBox.ShowCustomMessage("Account '" + txtName.Text + "' added succesfully", frmCustomMessageBox.MessageType.success);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding the account. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();
            }
        }
    }
}

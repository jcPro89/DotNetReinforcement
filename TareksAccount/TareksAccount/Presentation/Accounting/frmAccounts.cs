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
    public partial class frmAccounts : MetroForm
    {
        public frmAccounts()
        {
            InitializeComponent();
        }

        bool bDataIsLoaded=false;

        private void frmAccounts_Load(object sender, EventArgs e)
        {
            //LOAD GROUPS
            cbxGroup.DataSource = Logic.Accounting.GroupLogic.LoadAllLedgerGroups(frmLogin.iSelectedCompanyId);
            cbxGroup.DisplayMember = "Name";
            cbxGroup.ValueMember = "Id";


            //LOAD ALL ACCOUNTS
            dtgAccounts.DataSource = Logic.Accounting.AccountsLogic.LoadAllAccounts(frmLogin.iSelectedCompanyId);
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

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            Accounting.frmAccountAdd ofrm = new frmAccountAdd();
            ofrm.ShowDialog();
        }
    }
}

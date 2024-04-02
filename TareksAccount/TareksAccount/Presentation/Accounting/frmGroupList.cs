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
    public partial class frmGroupList : MetroForm
    {
        public frmGroupList()
        {
            InitializeComponent();
        }

        private void frmGroupList_Load(object sender, EventArgs e)
        {
            
           
        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            Accounting.frmCreateGroup ofrm = new frmCreateGroup();
            ofrm.ShowDialog();
        }

        private void frmGroupList_Activated(object sender, EventArgs e)
        {
            dtgGroups.DataSource = null;
            dtgGroups.Refresh();
            dtgGroups.DataSource = Logic.Accounting.GroupLogic.LoadAllLedgerGroups(frmLogin.iSelectedCompanyId);
            dtgGroups.Columns["Id"].Visible = false;
            dtgGroups.Columns["Code"].Width = 115;
            dtgGroups.Columns["Name"].Width = 320;
            dtgGroups.Columns["AccountType"].Width = 145;
            dtgGroups.Columns["SubAccounts"].Width = 65;
        }
    }
}

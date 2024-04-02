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
    public partial class frmSubAccountList : MetroForm
    {
        public frmSubAccountList()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCreateSubGroup_Click(object sender, EventArgs e)
        {
            Accounting.frmCreateSubGroup ofrm = new frmCreateSubGroup();
            ofrm.ShowDialog();
        }

        private void frmSubAccountList_Load(object sender, EventArgs e)
        {
            SearchSubGroupsByGroupId(0);
        }

        private void frmSubAccountList_Activated(object sender, EventArgs e)
        {
            

        }

        private void SearchSubGroupsByGroupId(int pGroupId)
        {
            //LOAD ALL SUBGROUPS
            dtgSubGroups.DataSource = null;
            dtgSubGroups.Refresh();
            dtgSubGroups.DataSource = Logic.Accounting.SubGroupLogic.SearchSubGroupsByGroupId(pGroupId);

            dtgSubGroups.Columns["Code"].Width = 135;
            dtgSubGroups.Columns["Name"].Width = 320;
            //dtgSubGroups.Columns["AccountType"].Width = 145;
            //dtgSubGroups.Columns["SubAccounts"].Width = 65;
        }
    }
}

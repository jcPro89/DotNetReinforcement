using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TareksAccount.Presentation.Clients
{
    public partial class frmClients : MetroForm
    {
        public frmClients()
        {
            InitializeComponent();
        }

        //FOR WHEN IT IS NEEDED FROM OTHER FORM
        public int ClientId;

        private void frmClients_Load(object sender, EventArgs e)
        {

            //LOAD CLIENT GROUPS
            DataTable dttAllClientGroups= Logic.Clients.ClientsLogic.AllClientGroups(frmLogin.iSelectedCompanyId);
            DataRow oAllRow = dttAllClientGroups.NewRow();
            oAllRow["Id"] = 0;
            oAllRow["Name"]= "<- All Groups ->";
            dttAllClientGroups.Rows.InsertAt(oAllRow, 0);
            //cmbGroups.DataSource = dttAllClientGroups;
            //cmbGroups.ValueMember = "Id";
            //cmbGroups.DisplayMember = "Name";

            //LOAD ALL CLIENTS
            dtgClients.DataSource = Logic.Clients.ClientsLogic.AllClients(frmLogin.iSelectedCompanyId);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmClientAddUpdate ofrm = new frmClientAddUpdate();
            ofrm.ShowDialog();
        }

        private void frmClients_Activated(object sender, EventArgs e)
        {
            //SEE ALL GROUPS
           //cmbGroups.SelectedValue = 0;


        }

        private void cmbGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            //if(cmbGroups.SelectedIndex==0)
            //{
            //    //LOAD ALL CLIENTS
            //    dtgClients.DataSource = null;
            //    dtgClients.DataSource = Logic.Clients.ClientsLogic.AllClients(frmLogin.iSelectedCompanyId);
            //}
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            frmClientAddUpdate ofrm = new frmClientAddUpdate();
            ofrm.ShowDialog();
        }
    }
}

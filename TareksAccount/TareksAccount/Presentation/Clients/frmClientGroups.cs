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
    public partial class frmClientGroups : MetroForm
    {
        public frmClientGroups()
        {
            InitializeComponent();
        }

        private void frmClientGroups_Load(object sender, EventArgs e)
        {
            //LOAD ALL GROUPS
            dtgClientGroups.DataSource= Logic.Clients.ClientsLogic.AllClientGroups(frmLogin.iSelectedCompanyId);
            dtgClientGroups.Columns["Id"].Visible = false;
            dtgClientGroups.Columns["Description"].Visible = false;
                    }

       

        private void dtgClientGroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgClientGroups.CurrentRow != null)
            {
                txtName.Text=Convert.ToString(dtgClientGroups.CurrentRow.Cells["Name"].Value);
                txtDescription.Text= Convert.ToString(dtgClientGroups.CurrentRow.Cells["Description"].Value);
            }
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            
                //SAVE NEW GROUP TO DATATABLE
                int oAffected = Logic.Clients.ClientsLogic.AddNewClientsGroup(txtName.Text, txtDescription.Text, frmLogin.iSelectedCompanyId);
                try
                {
                    if (oAffected == 1)
                    {
                        frmCustomMessageBox.ShowCustomMessage("Group '" + txtName.Text + "' succesfully added", frmCustomMessageBox.MessageType.success);
                        //   MessageBox.Show("Client Group added succesfully");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error while adding the new group. Error details: " + ex.Message);
                }

            
        }

        private void txtSaveChanges_Click(object sender, EventArgs e)
        {
            //UPDATE CLIENT GROUP INFO ON DATABASE


        }
    }
}


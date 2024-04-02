using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TareksAccount.Presentation.Projects
{
    public partial class frmProjects : Form
    {
        public frmProjects()
        {
            InitializeComponent();
        }

        private void frmProjects_Load(object sender, EventArgs e)
        {
            //LOAD ALL CLIENTS

            cmbClients.DataSource = Logic.Clients.ClientsLogic.AllClients(frmLogin.iSelectedCompanyId);
            cmbClients.ValueMember = "Id";
            cmbClients.DisplayMember = "Name";

            //LOAD ALL PROJECTS
            dtgProjects.DataSource = Logic.Projects.ProjectsLogic.AllProjects(frmLogin.iSelectedCompanyId);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
                { 
            //ADD NEW PROJECT TO DATABASE
            int oAffected = Logic.Projects.ProjectsLogic.AddProject(Convert.ToInt32( cmbClients.SelectedValue), txtName.Text, txtDescription.Text);
            if (oAffected == 1)
            {
                MessageBox.Show("Project added succesfully");
            }

        }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding the project. Error details: " + ex.Message);
                
                    Data.ConnectionData.DisconnectFromDatabase();
                
            }
}
    }
}

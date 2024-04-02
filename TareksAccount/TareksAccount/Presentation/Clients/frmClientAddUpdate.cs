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
    public partial class frmClientAddUpdate : MetroForm
    {
        public frmClientAddUpdate()
        {
            InitializeComponent();
        }

     

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if(chkActive.Checked)
            {
                chkActive.BackColor = Color.Green;
                chkActive.Text = "Active";
            }
            else
            {
                chkActive.BackColor = Color.Red;
                chkActive.Text = "Disabled";
            }
        }

        private void frmClientAdd_Load(object sender, EventArgs e)
        {
            try
            {
                //LOAD CLIENT GROUPS
                cbxGroup.DataSource = Logic.Clients.ClientsLogic.AllClientGroups(frmLogin.iSelectedCompanyId);
                cbxGroup.ValueMember = "Id";
                cbxGroup.DisplayMember = "Name";

                //LOAD CURRENCIES
                cbxCurrency.DataSource = Logic.Settings.SettingsLogic.AllCurrenciesCurrentExchangeRate();
                cbxCurrency.ValueMember = "CurrencyId";
                cbxCurrency.DisplayMember = "Code";

                //LOAD
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred when loading data. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();
            }
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int oAffected = Logic.Clients.ClientsLogic.AddClient(Convert.ToInt32(cbxGroup.SelectedValue), Convert.ToInt32(cbxCurrency.SelectedValue), txtSalesRep.Text, cbxTermsOfPayment.Text, txtName.Text, txtCountry.Text, txtCity.Text, txtAddress.Text, txtCompany.Text, txtPhone1.Text, txtPhone2.Text, txtEmail.Text, txtCC.Text, Convert.ToDouble(txtCreditLimit.Text), Convert.ToInt32(txtCreditDays.Text), txtSource.Text, txtNotes.Text, chkActive.Checked, dtClientSince.Value.Date);
                if (oAffected == 1)
                {
                    frmCustomMessageBox.ShowCustomMessage("Client added succesfully", frmCustomMessageBox.MessageType.success);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding the client. Error details: " + ex.Message);

                Data.ConnectionData.DisconnectFromDatabase();

            }
        }
    }
}

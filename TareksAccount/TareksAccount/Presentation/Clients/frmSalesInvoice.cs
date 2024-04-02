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
    public partial class frmSalesInvoice : MetroForm
    {
        public frmSalesInvoice()
        {
            InitializeComponent();
        }

        bool bInitialDataLoaded = false;



       
        private void frmSalesInvoice_Load(object sender, EventArgs e)
        {
            //LOAD ALL CLIENTS IN SEARCH AND INVOICE TABS
            DataTable dttAllClientsForSearch= Logic.Clients.ClientsLogic.AllClients(frmLogin.iSelectedCompanyId);

            cmbClientSearch.DataSource = dttAllClientsForSearch;
            cmbClientSearch.ValueMember = "Id";
            cmbClientSearch.DisplayMember = "Name";

            DataTable dttAllClientsForInvoice = dttAllClientsForSearch.Copy();

            cbxInvoiceClient.DataSource = dttAllClientsForInvoice;
            cbxInvoiceClient.ValueMember = "Id";
            cbxInvoiceClient.DisplayMember = "Name";


            //LOAD ALL CURRENCIES IN HEADER AND DETAILS TABS
            cmbInvoiceCurrency.DataSource = Logic.Settings.SettingsLogic.AllCurrenciesCurrentExchangeRate();
            cmbInvoiceCurrency.DisplayMember = "Code";
            cmbInvoiceCurrency.ValueMember = "CurrencyId";

            cbxLineCurrency.DataSource = Logic.Settings.SettingsLogic.AllCurrenciesCurrentExchangeRate();
            cbxLineCurrency.DisplayMember = "Code";
            cbxLineCurrency.ValueMember = "CurrencyId";

            //LOAD ALL PRODUCTS AND SERVICES
            rdbProductService.Checked = true;
           

                       
            bInitialDataLoaded = true;
                                            
        }


        private void cmbClientSearch_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    //LOAD CLIENT PROJECTS
                    DataTable dttClientProjects = Logic.Clients.SalesInvoicesLogic.SearchProjectsByClientId(Convert.ToInt32(cmbClientSearch.SelectedValue));
                    if (dttClientProjects != null && dttClientProjects.Rows.Count > 0)
                    {
                        cmbProjectSearch.DataSource = dttClientProjects;
                        cmbProjectSearch.ValueMember = "Id";
                        cmbProjectSearch.DisplayMember = "Name";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while loading projects. Error details: " + ex.Message);
            }
        }

        private void cmbClientOfInvoice_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    //LOAD CLIENT PROJECTS
                    DataTable dttClientProjects = Logic.Clients.SalesInvoicesLogic.SearchProjectsByClientId(Convert.ToInt32(cbxInvoiceClient.SelectedValue));
                    if (dttClientProjects != null && dttClientProjects.Rows.Count > 0)
                    {
                        cmbInvoiceProject.DataSource = dttClientProjects;
                        cmbInvoiceProject.ValueMember = "Id";
                        cmbInvoiceProject.DisplayMember = "Name";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while loading projects. Error details: " + ex.Message);
            }
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    //GET CURRENT EXCHANGE RATE
                    txtExchangeRate.Value = Logic.Clients.SalesInvoicesLogic.GetExchangeRate(Convert.ToInt32(cmbInvoiceCurrency.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while getting current exchange rate for " + cmbInvoiceCurrency.Text + ". Error details: " + ex.Message);
}
        }

        private void btnAddInvoiceLine_Click(object sender, EventArgs e)
        {
            //dtgSalesInvoiceLines.Rows.Add(cmbProductsAndServices.Items[cmbProductsAndServices.SelectedIndex]);
            //DataRowView vrow = (DataRowView)cmbProductsAndServices.SelectedItem;
            //DataRow row = vrow.Row;
            //dtgSalesInvoiceLines.Rows.Add(row);
            try
            {
                //Button btnDelete = new Button();
                //btnDelete.Text = "X";
                //btnDelete.TextAlign = ContentAlignment.MiddleCenter;

                //dtgSalesInvoiceLines.Rows.Add(
                //    cbxProductsAndServices.SelectedValue, // EXPENSE ID
                //    cbxProductsAndServices.Text,         // EXPENSE NAME
                //    DateTime.Now.ToShortDateString(),               // DATE OF LAST CHANGE TO LINE
                //    "TAREK",                                        // INCHARGE PERSON
                //    cbxLineCurrency.SelectedValue, //CURRENCY ID
                //    cbxLineCurrency.Text,                           //LINE CURRENCY
                //    nudFC_Amount.Value, // FOREIGN CURRENCY AMOUNT 
                //    nudLC_Amount.Value,                                                             // LOCAL CURRENCY AMOUNT
                //    nudClientCharge.Value                                                               // AMOUNT TO CHARGE TO CLIENT
                //    );

                nudNetClientCharge.Value += nudClientCharge.Value;
                nudNetInvoiceValue.Value += nudLC_Amount.Value;
                nudRealIncome.Value = nudNetClientCharge.Value - nudNetInvoiceValue.Value;
                
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while adding the line to the invoice. Error details: " + ex.Message);
            }
        }


        

      


        private void dtgSalesInvoiceLines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dtgSalesInvoiceLines.Columns[e.ColumnIndex].Name);
            if (dtgSalesInvoiceLines.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                
                nudNetClientCharge.Value -= Convert.ToDecimal(dtgSalesInvoiceLines.Rows[e.RowIndex].Cells["txtClientCharge"].Value);
                nudNetInvoiceValue.Value -= Convert.ToDecimal(dtgSalesInvoiceLines.Rows[e.RowIndex].Cells["txtLC_Amount"].Value);
                nudRealIncome.Value = nudNetClientCharge.Value - nudNetInvoiceValue.Value;
                    dtgSalesInvoiceLines.Rows.RemoveAt(e.RowIndex);
            }
        }

        decimal dLineExchangeRate = 0;
        private void cxbLineCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    //GET EXHANGE RATE FOR LINE CURRENCY
                    //dLineExchangeRate = Logic.Clients.SalesInvoicesLogic.GetExchangeRate(Convert.ToInt32(cbxLineCurrency.SelectedValue));
                    DataRowView drvSelectedCurrencyForInvoiceLine = (DataRowView)cbxLineCurrency.SelectedItem;


                    //UPDATE LC AMOUNT AND CLIENT CHARGE
                    nudFC_Amount.Value = nudLC_Amount.Value * Convert.ToDecimal(drvSelectedCurrencyForInvoiceLine["ExchangeRate"].ToString());
                    //nudFC_Amount.Value = nudLC_Amount.Value * dLineExchangeRate;

                }
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while converting currency. Error details: " + ex.Message);
            }
        }

        private void btnPrintExport_Click(object sender, EventArgs e)
        {
            ctxPrint.Show(btnPrintExport, new Point(0, btnPrintExport.Height));
        }



       // Shared_Entities.ProductService oSelectedExpense;

        private void cbxInvoiceLineItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    if (rdbProductService.Checked)
                    {
                        //LOAD PRODUCT/SERVICE INFO
                        DataTable dttProductServceInfo = Logic.Clients.SalesInvoicesLogic.SearchProductServiceById(Convert.ToInt32(cbxInvoiceLineItem.SelectedValue));
                        if (dttProductServceInfo != null && dttProductServceInfo.Rows.Count > 0)
                        {
                            ////SAVE CURRENT EXPENSE DATA IN MEMORY
                            //oSelectedExpense = new Shared_Entities.ProductService(
                            //    Convert.ToInt32(dttProductServceInfo.Rows[0]["Id"]),
                            //    Convert.ToString(dttProductServceInfo.Rows[0]["Name"]),
                            //    Convert.ToDecimal(dttProductServceInfo.Rows[0]["ExpenseCost"]),
                            //    Convert.ToDecimal(dttProductServceInfo.Rows[0]["SuggestedPrice"]));

                            //GET LC

                            cbxLineCurrency.SelectedValue = Convert.ToInt32(Logic.Settings.SettingsLogic.LocalCurrency().Rows[0]["Id"]);

                            nudLC_Amount.Value = Convert.ToDecimal(dttProductServceInfo.Rows[0]["ExpenseCost"]);
                            nudFC_Amount.Value = nudLC_Amount.Value; // EXCHANGE RATE 1.0 WHEN LOCAL CURRENCY
                            nudClientCharge.Value = Convert.ToDecimal(dttProductServceInfo.Rows[0]["SuggestedPrice"]);
                        }
                    }

                } 
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while loading item details. Error details: " + ex.Message);
            }
        }

        private void rdbProductService_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbProductService.Checked)
                {
                    try
                    {
                        //LOAD PRODUCTS AND SERVICES

                        cbxInvoiceLineItem.DataSource = Logic.Clients.ProductsAndServicesLogic.AllProductsAndServices(frmLogin.iSelectedCompanyId);
                        cbxInvoiceLineItem.ValueMember = "Id";
                        cbxInvoiceLineItem.DisplayMember = "Name";


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error ocurred while loading Products and Services. Error details: " + ex.Message);
                        Data.ConnectionData.DisconnectFromDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while loading products/services. Error details: " + ex.Message);
            }
        }

        private void rdbExpense_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbExpense.Checked)
                {
                    try
                    {
                        //LOAD OPERATION EXPENSES
                        cbxInvoiceLineItem.DataSource = null;
                        cbxInvoiceLineItem.DataSource = Logic.Accounting.AccountsLogic.AllOperationalExpensesByCompanyId(frmLogin.iSelectedCompanyId);
                        cbxInvoiceLineItem.DisplayMember = "Name";
                        cbxInvoiceLineItem.ValueMember = "Id";

                        nudFC_Amount.Value = 0;
                        nudLC_Amount.Value = 0;
                        nudClientCharge.Value = 0;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error ocurred while loading expenses. Error details: " + ex.Message);
                        Data.ConnectionData.DisconnectFromDatabase();
                    }
                }
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while loading expenses. Error details: " + ex.Message);
            }
        }

        private void nudFC_Amount_ValueChanged(object sender, EventArgs e)
        {
            DataRowView drvSelectedCurrencyForInvoiceLine = (DataRowView)cbxLineCurrency.SelectedItem;
            nudLC_Amount.Value = nudFC_Amount.Value / Convert.ToDecimal(drvSelectedCurrencyForInvoiceLine["ExchangeRate"].ToString());
        }

        private void cmbClientOfInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

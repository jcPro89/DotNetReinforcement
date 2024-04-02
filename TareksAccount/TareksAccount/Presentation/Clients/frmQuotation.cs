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
    public partial class frmQuotation : MetroForm
    {
        public frmQuotation()
        {
            InitializeComponent();
        }

        bool bInitialDataLoaded = false;
        private void frmQuotation_Load(object sender, EventArgs e)
        {
            //LOAD INITIAL DATA

            cbxClient.DataSource = Logic.Clients.ClientsLogic.AllClients(frmLogin.iSelectedCompanyId);
            cbxClient.DisplayMember = "Name";
            cbxClient.ValueMember = "Id";

            cbxStatus.DataSource = Logic.Clients.QuotationLogic.AllQuotationStatuses();
            cbxStatus.DisplayMember = "Status";
            cbxStatus.ValueMember = "Id";

            cbxQuotationCurrency.DataSource = Logic.Settings.SettingsLogic.AllCurrenciesCurrentExchangeRate();
            cbxQuotationCurrency.DisplayMember = "Code";
            cbxQuotationCurrency.ValueMember = "CurrencyId";

            cbxLineCurrency.DataSource = Logic.Settings.SettingsLogic.AllCurrenciesCurrentExchangeRate();
            cbxLineCurrency.DisplayMember = "Code";
            cbxLineCurrency.ValueMember = "CurrencyId";

            rdbProductService.Checked = true;

            bInitialDataLoaded = true;

            txtQuotationNo1.Text = "QT-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
            txtQuotationNo2.Text = Convert.ToString(Logic.Clients.QuotationLogic.GenerateNewQuotationNo(txtQuotationNo1.Text));

        }

        private void cbxClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    //LOAD CLIENT PROJECTS
                    DataTable dttClientProjects = Logic.Clients.SalesInvoicesLogic.SearchProjectsByClientId(Convert.ToInt32(cbxClient.SelectedValue));
                    if (dttClientProjects != null && dttClientProjects.Rows.Count > 0)
                    {
                        cbxProject.DataSource = dttClientProjects;
                        cbxProject.ValueMember = "Id";
                        cbxProject.DisplayMember = "Name";
                    }

                    //LOAD CLIENT CURRENCY
                    DataRowView oSelectedClient = (DataRowView)cbxClient.SelectedItem;
                    cbxQuotationCurrency.SelectedValue = Convert.ToInt32(oSelectedClient.Row["CurrencyId"].ToString());

                    //LOAD DATE OF CLIENT
                    dtCustomerSince.Value = Convert.ToDateTime(oSelectedClient.Row["ClientSince"].ToString());
                }
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while loading client data. Error details: " + ex.Message);
            }
        }

        private void cbxQuotationCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    DataRowView oSelectedQuotationCurrency = (DataRowView)cbxQuotationCurrency.SelectedItem;
                    nudQuotationExchangeRate.Value = Convert.ToDecimal(oSelectedQuotationCurrency.Row["ExchangeRate"].ToString());
                }
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while loading currency info. Error details: " + ex.Message);
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
                        cbxQuotationLineItem.DataSource = Logic.Clients.ProductsAndServicesLogic.AllProductsAndServices(frmLogin.iSelectedCompanyId);
                        cbxQuotationLineItem.ValueMember = "Id";
                        cbxQuotationLineItem.DisplayMember = "Name";


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
                        cbxQuotationLineItem.DataSource = null;
                        cbxQuotationLineItem.DataSource = Logic.Accounting.AccountsLogic.AllOperationalExpensesByCompanyId(frmLogin.iSelectedCompanyId);
                        cbxQuotationLineItem.DisplayMember = "Name";
                        cbxQuotationLineItem.ValueMember = "Id";

                        nudLineFC_Amount.Value = 0;
                        nudLineLC_Amount.Value = 0;
                        nudLineClientCharge.Value = 0;

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

        private void btnAddQuotationLine_Click(object sender, EventArgs e)
        {
            try
            {
            //    dtgSalesInvoiceLines.Rows.Add(cmbProductsAndServices.Items[cmbProductsAndServices.SelectedIndex]);
            //DataRowView vrow = (DataRowView)cmbProductsAndServices.SelectedItem;
            //DataRow row = vrow.Row;
            //dtgSalesInvoiceLines.Rows.Add(row);
            
                Button btnDelete = new Button();
                btnDelete.Text = "X";
                btnDelete.TextAlign = ContentAlignment.MiddleCenter;

                dtgQuotationLines.Rows.Add(
                    rdbExpense.Checked,   //IsExpense
                    rdbProductService.Checked,  //IsProductService
                    Convert.ToInt32(cbxQuotationLineItem.SelectedValue),  //ItemId
                    cbxQuotationLineItem.Text, //Item
                    frmLogin.iLoggedInUserId,  //InchargeID
                    frmLogin.sLoggedInUsername,  //Incharge
                    DateTime.Now.Date.ToShortDateString(), //Date
                    Convert.ToInt32(cbxLineCurrency.SelectedValue), //CurrencyId
                    cbxLineCurrency.Text, //Currency
                    nudLineFC_Amount.Value, //FC Amount
                    nudLineLC_Amount.Value, //LC Amount
                    nudLineClientCharge.Value, //Client Charge
                    btnDelete); //Delete button


                nudQuotationNetClientCharge.Value += nudLineClientCharge.Value;
                nudNetQuotationValue.Value += nudLineLC_Amount.Value;
                nudQuotationRealIncome.Value = nudQuotationNetClientCharge.Value - nudNetQuotationValue.Value;
                               

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding the line to the invoice. Error details: " + ex.Message);
            }
        }

        private void cbxInvoiceLineItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    if (rdbProductService.Checked)
                    {
                        //LOAD PRODUCT/SERVICE INFO
                        DataTable dttProductServceInfo = Logic.Clients.SalesInvoicesLogic.SearchProductServiceById(Convert.ToInt32(cbxQuotationLineItem.SelectedValue));
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

                            nudLineLC_Amount.Value= Convert.ToDecimal(dttProductServceInfo.Rows[0]["ExpenseCost"]);
                            nudLineFC_Amount.Value = nudLineLC_Amount.Value; // EXCHANGE RATE 1.0 WHEN LOCAL CURRENCY
                            nudLineClientCharge.Value= Convert.ToDecimal(dttProductServceInfo.Rows[0]["SuggestedPrice"]);
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

        private void cbxLineCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (bInitialDataLoaded)
                {
                    //GET EXHANGE RATE FOR LINE CURRENCY
                    //dLineExchangeRate = Logic.Clients.SalesInvoicesLogic.GetExchangeRate(Convert.ToInt32(cbxLineCurrency.SelectedValue));
                    DataRowView drvSelectedCurrencyForInvoiceLine = (DataRowView)cbxLineCurrency.SelectedItem;


                    //UPDATE LC AMOUNT AND CLIENT CHARGE
                    nudLineFC_Amount.Value= nudLineLC_Amount.Value* Convert.ToDecimal(drvSelectedCurrencyForInvoiceLine["ExchangeRate"].ToString());
                    //nudFC_Amount.Value = nudLC_Amount.Value * dLineExchangeRate;

                }
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                MessageBox.Show("An error ocurred while converting currency. Error details: " + ex.Message);
            }
        }

        private void nudLineLC_Amount_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nudLineFC_Amount_ValueChanged(object sender, EventArgs e)
        {
            DataRowView drvSelectedCurrencyForInvoiceLine = (DataRowView)cbxLineCurrency.SelectedItem;
            nudLineLC_Amount.Value= nudLineFC_Amount.Value/ Convert.ToDecimal(drvSelectedCurrencyForInvoiceLine["ExchangeRate"].ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //SAVE QUOTATION


        }

        private void btnSaveNewQuotation_Click(object sender, EventArgs e)
        {

            //CREATE LIST OF QUOTATION LINES
            List<Shared_Entities.QuotationLine> oQuotationLinesList = new List<Shared_Entities.QuotationLine>();

            foreach(DataGridViewRow dgrQuotationLine in dtgQuotationLines.Rows)
            {
                Shared_Entities.QuotationLine oQuotationLine = new Shared_Entities.QuotationLine();
                oQuotationLine.IsExpense = Convert.ToBoolean(dgrQuotationLine.Cells["IsExpense"].Value);
                oQuotationLine.IsProductService = Convert.ToBoolean(dgrQuotationLine.Cells["IsProductService"].Value);
                oQuotationLine.ItemId = Convert.ToInt32(dgrQuotationLine.Cells["ItemId"].Value);
                oQuotationLine.Incharge = Convert.ToInt32(dgrQuotationLine.Cells["InchargeId"].Value);
                oQuotationLine.Date = Convert.ToDateTime(dgrQuotationLine.Cells["Date"].Value);
                oQuotationLine.CurrencyId = Convert.ToInt32(dgrQuotationLine.Cells["CurrencyId"].Value);
                oQuotationLine.FC_Amount = Convert.ToDecimal(dgrQuotationLine.Cells["FC_Amount"].Value);
                oQuotationLine.LC_Amount = Convert.ToDecimal(dgrQuotationLine.Cells["LC_Amount"].Value);
                oQuotationLine.ClientCharge = Convert.ToDecimal(dgrQuotationLine.Cells["ClientCharge"].Value);
                oQuotationLinesList.Add(oQuotationLine);
            }
            
            //ADD QUOTATION 



            Logic.Clients.QuotationLogic.AddQuotation(
                Convert.ToInt32(cbxStatus.SelectedValue),
                Convert.ToInt32(cbxProject.SelectedValue),
                Convert.ToInt32(cbxQuotationCurrency.SelectedValue),
                txtCustRef.Text,
                txtQuotationNo1.Text,
                txtQuotationNo2.Text,
                dtDate.Value.Date,
                txtShipType.Text,
                txtBL_Reference.Text,
                txtRemarks.Text,
                txtAttention.Text,
                tglTaxable.Checked,
                DateTime.Now.Date.AddDays(Convert.ToInt32(txtValidFor.Text)),
                oQuotationLinesList);


            frmCustomMessageBox.ShowCustomMessage("The new quotation was succesfully saved", frmCustomMessageBox.MessageType.success);

        }

        private void btnPrintExport_Click(object sender, EventArgs e)
        {
            //PRINT QUOTATION LAYOUT


        }
    }
}

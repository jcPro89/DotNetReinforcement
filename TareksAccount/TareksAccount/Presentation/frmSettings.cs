using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.IO;


namespace TareksAccount.Presentation
{
    public partial class frmSettings : MetroForm
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        string savedImageName = AppDomain.CurrentDomain.BaseDirectory + "ImageFromDb.bmp";

        private void frmSettings_Load(object sender, EventArgs e)
        {
            //lstCompanies.Columns[0].
            
            //string savedImageName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase) + "\\ImageFromDb.bmp";
            
            

            //LOAD COMPANIES

            //lstCompanies.Items.Clear();
            DataTable dttAllCompanies = Logic.Settings.SettingsLogic.AllCompanies();
            dtgCompanies.DataSource = dttAllCompanies;
            dtgCompanies.Columns["Id"].Visible = false;
            dtgCompanies.Columns["Logo"].Visible = false;
            dtgCompanies.Columns["LegalName"].Visible = false;
            dtgCompanies.Columns["LegalAddress"].Visible = false;
            dtgCompanies.Columns["Phone"].Visible = false;
            dtgCompanies.Columns["Website"].Visible = false;
            dtgCompanies.Columns["Email"].Visible = false;
            dtgCompanies.Columns["VAT_Number"].Visible = false;
            dtgCompanies.Columns["RegistrationNo"].Visible = false;
            dtgCompanies.Columns["Code"].Visible = false;

            //var list = db.Companies.ToList();
            //foreach(DataRow  drCompany in dttAllCompanies.Rows)
            //{

            //    ListViewItem item = new ListViewItem(Convert.ToString(drCompany["Id"]));
            //   item.SubItems.Add(Convert.ToString(drCompany["Name"]));
            //    lstCompanies.Items.Add(item);
            //}



            

            //LOAD CURRENCIES
            LoadCurrencies();

            

            //dtpNewCurrencyExchangeRateDateFrom.Value = DateTime.Now;
            //dtpNewCurrencyExchangeRateDateTo.Value = DateTime.Now.AddDays(10);

        }

        private void LoadCurrencies()
        {
            //CURRENCIES / XR                    
            cbxCurrencyForNewER.DataSource = Logic.Settings.SettingsLogic.AllCurrencies();
            cbxCurrencyForNewER.DisplayMember = "Code";
            cbxCurrencyForNewER.ValueMember = "Id";

            //LOAD CURRENT CURRENCIES EXCHANGE RATE
            dtgCurrentExchangeRates.DataSource = Logic.Settings.SettingsLogic.AllCurrenciesCurrentExchangeRate();
            dtgCurrentExchangeRates.Columns["CurrencyId"].Visible = false;
            dtgCurrentExchangeRates.Columns["Name"].Visible = false;
            dtgCurrentExchangeRates.Columns["rn"].Visible = false;
            dtgCurrentExchangeRates.Columns["DateFrom"].Visible = false;
            dtgCurrentExchangeRates.Columns["Code"].Width = 50;
            dtgCurrentExchangeRates.Columns["ExchangeRateId"].Visible = false;

        }

        private void dtgCurrencies_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgCurrentExchangeRates.CurrentRow != null)
            {
                txtModifyExchangeRateCode.Text = Convert.ToString(dtgCurrentExchangeRates.CurrentRow.Cells["Code"].Value);                
                nudModifyCurrentExchangeRate.Value = Convert.ToDecimal(dtgCurrentExchangeRates.CurrentRow.Cells["ExchangeRate"].Value);

                
                dtpModifyCurrentDateFrom.Value = Convert.ToDateTime(dtgCurrentExchangeRates.CurrentRow.Cells["DateFrom"].Value);
                dtpModifyCurrentDateTo.Value= Convert.ToDateTime(dtgCurrentExchangeRates.CurrentRow.Cells["DateTo"].Value);

            }
        }

        private void btnFullExchangeRateHistory_Click(object sender, EventArgs e)
        {
            frmFullExchangeRateHistory ofrm = new frmFullExchangeRateHistory();
            ofrm.ShowDialog();
        }

      

        private void btnAddNewCurrency_Click(object sender, EventArgs e)
        {
            //SAVE NEW CURRENCY WITH ITS ER
            try
            {
                int oAffected = Logic.Settings.SettingsLogic.AddCurrency(txtNewCurrencyCode.Text, txtNewCurrencyName.Text, nudNewCurrencyExchangeRate.Value, dtNewCurrencyFromDate.Value.Date, dtNewCurrencyToDate.Value.Date);
                if(oAffected==1)
                {
                    frmCustomMessageBox.ShowCustomMessage("New currency succesfully added", frmCustomMessageBox.MessageType.success);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding the new currency. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            openLogoFile.Title = "Select image for logo";

            if (openLogoFile.ShowDialog() == DialogResult.OK)
            {
                picCompanyLogo.Image = new Bitmap(openLogoFile.FileName);
                savedImageName = openLogoFile.FileName;
            }
        }


        private void btnSaveCompanyDetails_Click(object sender, EventArgs e)
        {

            try
            {
                MemoryStream tmpStream = new MemoryStream();
                picCompanyLogo.Image.Save(tmpStream, ImageFormat.Png); // change to other format
                tmpStream.Seek(0, SeekOrigin.Begin);
                FileStream fsImage = new FileStream(savedImageName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] imgBytes = new byte[fsImage.Length];
                fsImage.Read(imgBytes, 0, Convert.ToInt32(fsImage.Length));
                fsImage.Close();

                
               
                    int oAffected = Logic.Settings.SettingsLogic.UpdateCompanyDetails(frmLogin.iSelectedCompanyId, txtCompanyName.Text, imgBytes, txtPhone.Text, txtEmail.Text, txtWebsite.Text, txtCode.Text, txtRegistrationNo.Text, txtVAT.Text, txtLegalName.Text, txtLegalAddress.Text);
                    if (oAffected == 1)
                    {
                        frmCustomMessageBox.ShowCustomMessage("Company details succesfully saved", frmCustomMessageBox.MessageType.success);
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while saving company details. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();
            }
        }

        

        

        private void dtgCompanies_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgCompanies.CurrentRow != null)
            {
                Image curImage = null;

                txtCompanyName.Text = Convert.ToString(dtgCompanies["Name", dtgCompanies.CurrentRow.Index].Value);
                txtPhone.Text = Convert.ToString(dtgCompanies["Phone", dtgCompanies.CurrentRow.Index].Value);
                txtEmail.Text = Convert.ToString(dtgCompanies["Email", dtgCompanies.CurrentRow.Index].Value);
                txtCode.Text = Convert.ToString(dtgCompanies["Code", dtgCompanies.CurrentRow.Index].Value);
                txtLegalAddress.Text = Convert.ToString(dtgCompanies["LegalAddress", dtgCompanies.CurrentRow.Index].Value);
                txtLegalName.Text = Convert.ToString(dtgCompanies["LegalName", dtgCompanies.CurrentRow.Index].Value);
                txtWebsite.Text = Convert.ToString(dtgCompanies["Website", dtgCompanies.CurrentRow.Index].Value);
                txtRegistrationNo.Text = Convert.ToString(dtgCompanies["RegistrationNo", dtgCompanies.CurrentRow.Index].Value);
                txtVAT.Text = Convert.ToString(dtgCompanies["VAT_Number", dtgCompanies.CurrentRow.Index].Value);

                //DataTable dttCompanyDetails = Logic.Settings.SettingsLogic.GetCompanyDetails(frmLogin.iSelectedCompanyId);
                //txtCompanyName.Text = Convert.ToString(dttCompanyDetails.Rows[0]["Name"]);

                if (dtgCompanies.Rows[0].Cells["Logo"].Value != DBNull.Value)
                {
                    byte[] rawData = new byte[0];
                    rawData = (byte[]) dtgCompanies["Logo",dtgCompanies.CurrentRow.Index].Value;
                    int len = new int();

                    len = rawData.GetUpperBound(0);

                    //Save rawData as a bitmap
                    picCompanyLogo.Image = null;


                Image logoToDisplay = null;
                var imageMemoryStream = new MemoryStream(rawData);
                Image imgFromStream = Image.FromStream(imageMemoryStream);
                picCompanyLogo.Image = imgFromStream;    

                    //FileStream fs = new FileStream

                    //(savedImageName, FileMode.OpenOrCreate,

                    //FileAccess.Write);

                    //fs.Write(rawData, 0, len);

                    ////Close the stream

                    //fs.Close();
                    //fs.Dispose();

                    //View the image in a pciture box

                    //curImage = Image.FromFile(savedImageName);



                    //picCompanyLogo.Image = curImage;



               }
            }
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            picCompanyLogo.Image = null; // RELEASE USE OF IMAGE FILE OR LATER 
        }

        private void btnSaveNewCompany_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream tmpStream = new MemoryStream();
                picCompanyLogo.Image.Save(tmpStream, ImageFormat.Png); // change to other format
                tmpStream.Seek(0, SeekOrigin.Begin);
                FileStream fsImage = new FileStream(savedImageName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                byte[] imgBytes = new byte[fsImage.Length];
                fsImage.Read(imgBytes, 0, Convert.ToInt32(fsImage.Length));
                fsImage.Close();



                int oAffected = Logic.Settings.SettingsLogic.AddCompany(txtCompanyName.Text, imgBytes, txtPhone.Text, txtEmail.Text, txtWebsite.Text, txtCode.Text, txtRegistrationNo.Text, txtVAT.Text, txtLegalName.Text, txtLegalAddress.Text);
                if (oAffected == 1)
                {
                    frmCustomMessageBox.ShowCustomMessage("Company " + txtCompanyName.Text + " succesfully added", frmCustomMessageBox.MessageType.success);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while saving company details. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();
            }
        }

        private void btnAddNewExchangeRate_Click(object sender, EventArgs e)
        {
            //SAVE NEW ER FOR SELECTED CURRENCY
            try
            {
                int oAffected = Logic.Settings.SettingsLogic.AddCurrencyExchangeRate(Convert.ToInt32(cbxCurrencyForNewER.SelectedValue), nudNewExchangeRate.Value, dtNewExchangeRateFromDate.Value.Date, dtNewExchangeRateToDate.Value.Date);
                if (oAffected == 1)
                {
                    frmCustomMessageBox.ShowCustomMessage("New exchange rate succesfully added", frmCustomMessageBox.MessageType.success);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding the new exchange rate. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();
            }
        }

        private void btnModifyCurrentExchangeRate_Click(object sender, EventArgs e)
        {
           //MODIFY CURRENT SELECTED EXCHANGE RATE
            try
            {
                int oAffected = Logic.Settings.SettingsLogic.ModifyCurrencyExchangeRate(Convert.ToInt32(dtgCurrentExchangeRates.CurrentRow.Cells["ExchangeRateId"].Value), nudModifyCurrentExchangeRate.Value, dtpModifyCurrentDateFrom.Value.Date, dtpModifyCurrentDateTo.Value.Date);
                if (oAffected == 1)
                {
                    frmCustomMessageBox.ShowCustomMessage("Exchange rate succesfully updated", frmCustomMessageBox.MessageType.success);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while updating the exchange rate. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();
            }
        }
    }
}

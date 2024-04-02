using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace TareksAccount
{
    public partial class frmLogin : MaterialForm
    {
        public frmLogin()
        {
            InitializeComponent();
       
            MaterialSkin.MaterialSkinManager matSkinManager = MaterialSkin.MaterialSkinManager.Instance;
            matSkinManager.AddFormToManage(this);
            matSkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            matSkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Blue300, MaterialSkin.Primary.Blue500, MaterialSkin.Primary.Blue500, MaterialSkin.Accent.LightBlue400, MaterialSkin.TextShade.WHITE);

        }

        /// <summary>
        /// SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
        /// </summary>

        public static int iLoggedInUserId;
        public static int iSelectedCompanyId;
        public static string sLoggedInUsername;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // CONNECT TO DATABASE
                Data.ConnectionData.ConnectToDataBase();

                //LOAD COMPANIES
                cbxCompany.DataSource = Logic.Settings.SettingsLogic.AllCompanies();
                cbxCompany.DisplayMember = "Name";
                cbxCompany.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem ocurred when connecting to database. Internal error: " + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Data.ConnectionData.DisconnectFromDatabase();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dttUserData = Logic.LoginLogic.LoginData(txtUsername.Text);
                if (dttUserData != null && dttUserData.Rows.Count > 0)
                {
                    if (Convert.ToString(dttUserData.Rows[0]["Password"]) == txtPassword.Text)
                    {
                        //SAVE GLOBAL VARIABLES
                        iLoggedInUserId = Convert.ToInt32(dttUserData.Rows[0]["Id"]);
                        iSelectedCompanyId = Convert.ToInt32(cbxCompany.SelectedValue);
                        sLoggedInUsername = txtUsername.Text;

                        Presentation.frmMainMenu ofrm = new Presentation.frmMainMenu();
                        ofrm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect username or password entered. Please verify.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred when attempting to login. Error details: " + ex.Message);
            }
        }
    }
}

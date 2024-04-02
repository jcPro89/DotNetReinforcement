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
    public partial class frmProductsAndServices : MetroForm
    {
        public frmProductsAndServices()
        {
            InitializeComponent();
        }

        private void ProductsAndServices_Load(object sender, EventArgs e)
        {

            try
            {
                //LOAD ALL PRODUCTS AND SERVICES
                dtgProductsAndServices.DataSource = Logic.Clients.ProductsAndServicesLogic.AllProductsAndServices(frmLogin.iSelectedCompanyId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has ocurred while loading products and services. Error details: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //SAVE NEW PRODUCT/SERVICE
                int oAffected = Logic.Clients.ProductsAndServicesLogic.AddProductService(txtName.Text, txtDescription.Text, nudExpenseCost.Value, nudPrice.Value, frmLogin.iSelectedCompanyId);
                if (oAffected == 1)
                {
                    frmCustomMessageBox.ShowCustomMessage("Product/Service added succesfully", frmCustomMessageBox.MessageType.success);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocurred while adding the product/service. Error details: " + ex.Message);
                Data.ConnectionData.DisconnectFromDatabase();

            }

        }


    }
}

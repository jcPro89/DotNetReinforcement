using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TareksAccount.Presentation
{
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void ledgersGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addNewClientGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listOfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.TestReport ofrm = new Reports.TestReport();
            ofrm.ShowDialog();
        }

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.frmGroupList ofrm = new Accounting.frmGroupList();
            ofrm.ShowDialog();
        }

        private void subGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.frmSubAccountList ofrm = new Accounting.frmSubAccountList();
            ofrm.ShowDialog();
        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.frmPayments ofrm = new Accounting.frmPayments();
            ofrm.ShowDialog();
        }

        private void accountingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings ofrm = new frmSettings();
            ofrm.ShowDialog();
        }

        private void clientGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients.frmClientGroups ofrm = new Clients.frmClientGroups();
            ofrm.ShowDialog();
        }

        private void viewClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients.frmClients ofrm = new Clients.frmClients();
            ofrm.ShowDialog();
        }

        private void productsAndServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients.frmProductsAndServices ofrm = new Clients.frmProductsAndServices();
            ofrm.ShowDialog();
        }

        private void manageProijectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Projects.frmProjects ofrm = new Projects.frmProjects();
            ofrm.ShowDialog();
        }

        private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients.frmSalesInvoice ofrm = new Clients.frmSalesInvoice();
            ofrm.ShowDialog();
        }

        private void creditDebitNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.frmCreditDebit ofrm = new Accounting.frmCreditDebit();
            ofrm.ShowDialog();
        }

        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.frmAccounts ofrm = new Accounting.frmAccounts();
            ofrm.ShowDialog();
        }

        private void quotationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clients.frmQuotation ofrm = new Clients.frmQuotation();
            ofrm.ShowDialog();

        }
    }
}

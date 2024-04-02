using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices;

namespace TareksAccount.Presentation.Reports
{
    public partial class TestReport : Form
    {
        public TestReport()
        {
            InitializeComponent();
        }

        private void TestReport_Load(object sender, EventArgs e)
        {
            //string pathreport = Server.MapPath("~/reportes/reporte.rdlc");

            //reportViewer1.LocalReport.ReportPath = pathreport;

            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            ServerReport serverReport = reportViewer1.ServerReport;

            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            ReportServerCredentials rsCredentials = serverReport.ReportServerCredentials;
            rsCredentials.NetworkCredentials = credentials;

            serverReport.ReportServerUrl = new Uri("http://localhost/ReportServer");
            serverReport.ReportPath = "/Reports/TestReport";
            serverReport.HistoryId = null;
            this.reportViewer1.RefreshReport();
            

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

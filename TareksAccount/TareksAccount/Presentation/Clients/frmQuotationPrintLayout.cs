using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace TareksAccount.Presentation.Clients
{
    public partial class frmQuotationPrintLayout : Form
    {
        public frmQuotationPrintLayout()
        {
            InitializeComponent();
        }

        private void frmQuotationPrintLayout_Load(object sender, EventArgs e)
        {
            // Set Processing Mode
            reportViewer1.ProcessingMode = ProcessingMode.Remote;

            // Set report server and report path
            reportViewer1.ServerReport.ReportServerUrl = new
            Uri("http://localhost/reportserver");
            reportViewer1.ServerReport.ReportPath =
              "/AdventureWorks Sample Reports/Employee Sales Summary";

            // Display the parameters for this report
            //DumpParameterInfo(reportViewer1.ServerReport);

            // Set the parameters for this report
            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("EmpID", "288", false));
            paramList.Add(new ReportParameter("ReportMonth", "12", false));
            paramList.Add(new ReportParameter("ReportYear", "2003", false));

            this.reportViewer1.ServerReport.SetParameters(paramList);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}

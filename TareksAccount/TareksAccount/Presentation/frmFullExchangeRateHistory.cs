using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TareksAccount.Presentation
{
    public partial class frmFullExchangeRateHistory : Form
    {
        public frmFullExchangeRateHistory()
        {
            InitializeComponent();
        }

        private void frmFullExchangeRateHistory_Load(object sender, EventArgs e)
        {
            dtgFullExchangeRateHistory.DataSource = Logic.Settings.SettingsLogic.AllCurrenciesExchangeRateHistory();
        }
    }
}

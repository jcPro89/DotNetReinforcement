using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TareksAccount.Logic.Clients
{
    class SalesInvoicesLogic
    {
        public static DataTable SearchProjectsByClientId(int pClientId)
        {
            return Data.Clients.SalesInvoiceData.SearchProjectsByClientId(pClientId);
        }

        public static decimal GetExchangeRate(int pCurrencyId)
        {
            return Data.Clients.SalesInvoiceData.GetExchangeRate(pCurrencyId);
        }

        public static DataTable SearchProductServiceById(int pExpenseId)
        {
            return Data.Clients.SalesInvoiceData.SearchProductServiceById(pExpenseId);
        }
        }
}

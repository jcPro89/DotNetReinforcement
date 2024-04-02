using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareksAccount.Logic.Accounting
{
    class AccountsLogic
    {
        public static DataTable LoadAllAccounts(int pCompanyId)
        {
            return Data.Accounting.AccountsData.LoadAllAccounts(pCompanyId);
        }
    

    public static int AddAccount(int pSubGroupId, string pName, string pDescription)
    {
        return Data.Accounting.AccountsData.AddAccount(pSubGroupId, pName, pDescription);
    }

        public static DataTable AllOperationalExpensesByCompanyId(int pCompanyId)
        {
            return Data.Accounting.AccountsData.AllOperationalExpensesByCompanyId(pCompanyId);
        }
        }
}

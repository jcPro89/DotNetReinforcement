using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TareksAccount.Logic.Accounting
{
    class GroupLogic
    {
        public static DataTable LoadAllLedgerGroups(int pCompanyId)
        {
            return Data.Accounting.GroupData.LoadAllLedgerGroups(pCompanyId);
        }
        public static int CreateLedgerGroup(string pCode, string pName, string pDescription, int pTypeId, bool pAcceptsSubAccounts, bool pPL_Account, bool pIsActive, int pCompanyId)
        {
            return Data.Accounting.GroupData.CreateLedgerGroup(pCode, pName, pDescription, pTypeId, pAcceptsSubAccounts, pPL_Account, pIsActive, pCompanyId);
        }
        public static DataTable LoadAccountTypes()
        {
            return Data.Accounting.GroupData.LoadAccountTypes();
        }

       
        }
}

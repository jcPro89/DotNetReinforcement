using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareksAccount.Logic.Accounting
{
    class SubGroupLogic
    {
        public static DataTable SearchSubGroupsByGroupId(int pGroupId)
        {
            return Data.Accounting.SubGroupData.SearchSubGroupsByGroupId(pGroupId);
        }


        public static int CreateLedgerSubGroup(string pCode, string pName, int pGroupId, string pDescription, bool pIsActive)
        {
            return Data.Accounting.SubGroupData.CreateLedgerSubGroup(pCode, pName, pGroupId, pDescription, pIsActive);
        }
    }
}

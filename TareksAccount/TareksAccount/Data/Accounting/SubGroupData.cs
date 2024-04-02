using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareksAccount.Data.Accounting
{
    class SubGroupData
    {
        public static DataTable SearchSubGroupsByGroupId(int pGroupId)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("SearchSubGroupsByGroupId", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@GroupId", pGroupId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttSubAccounts = new DataTable();
                oAdapter.Fill(dttSubAccounts);
                return dttSubAccounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public static int CreateLedgerSubGroup(string pCode, string pName, int pGroupId, string pDescription, bool pIsActive)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AddLedgerSubGroup", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@Code", pCode);
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Description", pDescription);
                oCommand.Parameters.AddWithValue("@GroupId", pGroupId);
                oCommand.Parameters.AddWithValue("@IsActive", pIsActive);
                int oAffected = oCommand.ExecuteNonQuery();
                if (oAffected == 1)
                {
                    return 1;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

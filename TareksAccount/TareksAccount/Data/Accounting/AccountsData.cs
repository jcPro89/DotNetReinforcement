
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareksAccount.Data.Accounting
{
    class AccountsData
    {
        public static DataTable LoadAllAccounts(int pCompanyId)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllAccounts", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllAccounts = new DataTable();
                oAdapter.Fill(dttAllAccounts);
                return dttAllAccounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int AddAccount(int pSubGroupId, string pName, string pDescription)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AddAccount", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@SubgroupId", pSubGroupId);
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Description", pDescription);
                Data.ConnectionData.ConnectToDataBase();
                int oAffected = oCommand.ExecuteNonQuery();
                Data.ConnectionData.DisconnectFromDatabase();
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


        public static DataTable AllOperationalExpensesByCompanyId(int pCompanyId)
        {
            try
            {

                SqlCommand oCommand = new SqlCommand("AllOperationalExpensesByCompanyId", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                //oAdapter.Fill(new DataTable());

                //return new DataTable();

                DataTable dtExpenses = new DataTable();
                oAdapter.Fill(dtExpenses);
                return dtExpenses;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

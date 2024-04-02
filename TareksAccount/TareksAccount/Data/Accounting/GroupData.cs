using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TareksAccount.Data.Accounting
{
    class GroupData
    {
        public static DataTable LoadAllLedgerGroups(int pCompanyId)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllAccountGroups", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllGroups = new DataTable();
                oAdapter.Fill(dttAllGroups);
                return dttAllGroups;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int CreateLedgerGroup(string pCode, string pName, string pDescription, int pTypeId, bool pAcceptsSubAccounts, bool pPL_Account, bool pIsActive, int pCompanyId)
        {
            SqlTransaction oTransaction = null;
            try
            {
                SqlCommand oCommand = new SqlCommand("AddLedgerGroup", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@Code", pCode);
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Description", pDescription);
                oCommand.Parameters.AddWithValue("@TypeId", pTypeId);
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                oCommand.Parameters.AddWithValue("@PL_Account", pPL_Account);
                oCommand.Parameters.AddWithValue("@AcceptsSubAccounts", pAcceptsSubAccounts);
                oCommand.Parameters.AddWithValue("@IsActive", pIsActive);
                Data.ConnectionData.ConnectToDataBase();
                oTransaction = Data.ConnectionData.oConnection.BeginTransaction();
                oCommand.Transaction = oTransaction;

                int oAffected = oCommand.ExecuteNonQuery();

                if (oAffected == 2) //SUBGROUP 'NONE' ALSO ADDED
                {
                    oTransaction.Commit();
                    Data.ConnectionData.DisconnectFromDatabase();
                    return 1;
                }
                else
                {
                    if(oTransaction!=null)
                        oTransaction.Rollback();
                    Data.ConnectionData.DisconnectFromDatabase();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                }
                Data.ConnectionData.DisconnectFromDatabase();
                throw ex;

            }

        }

        public static DataTable LoadAccountTypes ()
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllAccountTypes", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllAccountTypes = new DataTable();
                oAdapter.Fill(dttAllAccountTypes);
                return dttAllAccountTypes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}

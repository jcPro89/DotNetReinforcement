using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace TareksAccount.Data.Clients
{
    class ClientsData
    {
        public static DataTable AllClientGroups(int pCompanyId)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllClientGroups", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllClientGroups = new DataTable();
                oAdapter.Fill(dttAllClientGroups);
                return dttAllClientGroups;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static int AddNewClientsGroup(string pName, string pDescription, int pCompanyId)
        {
            try
            {

                SqlCommand oCommand = new SqlCommand("AddClientGroup", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;

            
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Description", pDescription);
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);

                Data.ConnectionData.ConnectToDataBase();          
                int oAffected = oCommand.ExecuteNonQuery();
                Data.ConnectionData.DisconnectFromDatabase();
                if (oAffected == 1) 
                {
                    
                    return 1;
                }
                else
                {
                    
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }

        public static DataTable AllClients (int pCompanyId)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllClients", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllClients = new DataTable();
                oAdapter.Fill(dttAllClients);
                return dttAllClients;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int AddClient (int pGroupId, int pCurrencyId, string pSalesRep, string pTermsOfPayment, string pName, string pCountry, string pCity, string pAddress, string pCompanyName, string pPhone, string pAltPhone, string pEmail, string pCC, double pCreditLimit, int pCreditDays, string pSourceOfClient, string pNotes, bool pStatus, DateTime pClientSince)
        {
            try
            {

                SqlCommand oCommand = new SqlCommand("AddClient", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;

                oCommand.Parameters.AddWithValue("@ClientGroupId", pGroupId);
                oCommand.Parameters.AddWithValue("@CurrencyId", pCurrencyId);
                oCommand.Parameters.AddWithValue("@SalesRep", pSalesRep);
                oCommand.Parameters.AddWithValue("@TermsOfPayment", pTermsOfPayment);
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Country", pCountry);
                oCommand.Parameters.AddWithValue("@City", pCity);
                oCommand.Parameters.AddWithValue("@Address", pAddress);
                oCommand.Parameters.AddWithValue("@CompanyName", pCompanyName);
                oCommand.Parameters.AddWithValue("@Phone", pPhone);
                oCommand.Parameters.AddWithValue("@AlternativePhone", pAltPhone);
                oCommand.Parameters.AddWithValue("@Email", pEmail);
                oCommand.Parameters.AddWithValue("@CC", pCC);
                oCommand.Parameters.AddWithValue("@CreditLimit", pCreditLimit);
                oCommand.Parameters.AddWithValue("@CreditDays", pCreditDays);
                oCommand.Parameters.AddWithValue("@SourceOfClient", pSourceOfClient);
                oCommand.Parameters.AddWithValue("@Notes", pNotes);
                oCommand.Parameters.AddWithValue("@Status", pStatus);
                oCommand.Parameters.AddWithValue("@ClientSince", pClientSince);

                Data.ConnectionData.ConnectToDataBase();
                int oAffected = oCommand.ExecuteNonQuery();
                Data.ConnectionData.DisconnectFromDatabase();
                if (oAffected == 1)
                {

                    return 1;
                }
                else
                {

                    return 0;
                }
            }
            catch (Exception ex)
            {
                if (Data.ConnectionData.oConnection.State != ConnectionState.Closed)
                    Data.ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }
    }
}

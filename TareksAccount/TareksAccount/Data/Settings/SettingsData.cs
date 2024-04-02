using System;
using System.Data;
using System.Data.SqlClient;

namespace TareksAccount.Data.Settings
{
    class SettingsData
    {

        public static DataTable AllCurrencies()
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllCurrencies", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllCurrencies = new DataTable();
                oAdapter.Fill(dttAllCurrencies);
                return dttAllCurrencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable AllCurrenciesCurrentExchangeRate()
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllCurrenciesCurrentExchangeRate", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllCurrencies = new DataTable();
                oAdapter.Fill(dttAllCurrencies);
                return dttAllCurrencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable AllCurrenciesExchangeRateHistory ()
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllCurrenciesExchangeRateHistory", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllCurrencies = new DataTable();
                oAdapter.Fill(dttAllCurrencies);
                return dttAllCurrencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable LocalCurrency ()
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("LocalCurrency", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttLocalCurrency = new DataTable();
                oAdapter.Fill(dttLocalCurrency);
                return dttLocalCurrency;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int AddCurrency(string pCode, string pName, decimal pExchangeRate, DateTime pDateFrom, DateTime pDateTo)
        {
            SqlTransaction oTransaction = null;
            try
            {
                
                SqlCommand oCommand = new SqlCommand("AddCurrency", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
              
                oCommand.Parameters.AddWithValue("@Code", pCode);
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@ExchangeRate", pExchangeRate);
                oCommand.Parameters.AddWithValue("@DateFrom", pDateFrom);
                oCommand.Parameters.AddWithValue("@DateTo", pDateTo);

                Data.ConnectionData.ConnectToDataBase();
                oTransaction = Data.ConnectionData.oConnection.BeginTransaction();
                oCommand.Transaction = oTransaction;                
                int oAffected = oCommand.ExecuteNonQuery();
                if (oAffected == 2) //TWO INSERTS
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
                    oTransaction.Rollback();
                throw ex;
            }
        }

        public static int AddCurrencyExchangeRate(int pCurrencyId, decimal pExchangeRate, DateTime pDateFrom, DateTime pDateTo)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AddCurrencyExchangeRate", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CurrencyId", pCurrencyId);
                oCommand.Parameters.AddWithValue("@ExchangeRate", pExchangeRate);
                oCommand.Parameters.AddWithValue("@DateFrom", pDateFrom);
                oCommand.Parameters.AddWithValue("@DateTo", pDateTo);
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
                throw ex;
            }
        }

        public static int ModifyCurrencyExchangeRate(int pExchangeRateId, decimal pExchangeRate, DateTime pDateFrom, DateTime pDateTo)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("ModifyCurrencyExchangeRate", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ExchangeRateId", pExchangeRateId);
                oCommand.Parameters.AddWithValue("@ExchangeRate", pExchangeRate);
                oCommand.Parameters.AddWithValue("@DateFrom", pDateFrom);
                oCommand.Parameters.AddWithValue("@DateTo", pDateTo);
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
                throw ex;
            }
        }


                public static int AddCompany(string pName, byte[] pLogo, string pPhone, string pEmail, string pWebsite, string pCode, string pRegistrationNo, string pVAT_Number, string pLegalName, string pLegalAddress)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AddCompany", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                
                
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Logo", pLogo);
                oCommand.Parameters.AddWithValue("@Phone", pPhone);
                oCommand.Parameters.AddWithValue("@Email", pEmail);
                oCommand.Parameters.AddWithValue("@Website", pWebsite);
                oCommand.Parameters.AddWithValue("@Code",pCode);
                oCommand.Parameters.AddWithValue("@RegistrationNo", pRegistrationNo);
                oCommand.Parameters.AddWithValue("@VAT_Number", pVAT_Number);
                oCommand.Parameters.AddWithValue("@LegalAddress", pLegalAddress);
                oCommand.Parameters.AddWithValue("@LegalName", pLegalName);

                ConnectionData.ConnectToDataBase();
                          int oAffected = oCommand.ExecuteNonQuery();
                ConnectionData.DisconnectFromDatabase();

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
                ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }

        public static int UpdateCompanyDetails(int pCompanyId, string pName, byte[] pLogo, string pPhone, string pEmail, string pWebsite, string pCode, string pRegistrationNo, string pVAT_Number, string pLegalName, string pLegalAddress)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("UpdateCompanyInfo", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;

                oCommand.Parameters.AddWithValue("CompanyId", pCompanyId);
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Logo", pLogo);
                oCommand.Parameters.AddWithValue("@Phone", pPhone);
                oCommand.Parameters.AddWithValue("@Email", pEmail);
                oCommand.Parameters.AddWithValue("@Website", pWebsite);
                oCommand.Parameters.AddWithValue("@Code", pCode);
                oCommand.Parameters.AddWithValue("@RegistrationNo", pRegistrationNo);
                oCommand.Parameters.AddWithValue("@VAT_Number", pVAT_Number);
                oCommand.Parameters.AddWithValue("@LegalAddress", pLegalAddress);
                oCommand.Parameters.AddWithValue("@LegalName", pLegalName);

                ConnectionData.ConnectToDataBase();
                int oAffected = oCommand.ExecuteNonQuery();
                ConnectionData.DisconnectFromDatabase();

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
                ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }


        public static DataTable GetCompanyDetails(int pCompanyId)
        {
          
                try
                {
                    SqlCommand oCommand = new SqlCommand("GetCompanyDetails", Data.ConnectionData.oConnection);
                    oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                    SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                    DataTable dttCompanyDetails = new DataTable();
                    oAdapter.Fill(dttCompanyDetails);
                    return dttCompanyDetails;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            
        }

         public static DataTable AllCompanies()
        {
          
                try
                {
                    SqlCommand oCommand = new SqlCommand("AllCompanies", Data.ConnectionData.oConnection);
                    oCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                    DataTable dttCompanyDetails = new DataTable();
                    oAdapter.Fill(dttCompanyDetails);
                    return dttCompanyDetails;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            
        }
    }
}

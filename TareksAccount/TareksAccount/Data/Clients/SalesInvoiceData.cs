using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TareksAccount.Data.Clients
{
    class SalesInvoiceData
    {
        public static DataTable SearchProjectsByClientId(int pClientId)
        {
            try
            {


                SqlCommand oCommand = new SqlCommand("SearchProjectsByClientId", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@ClientId", pClientId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttClientProjects = new DataTable();
                oAdapter.Fill(dttClientProjects);
                return dttClientProjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal GetExchangeRate (int pCurrencyId)
        {
            try
            {
                
                SqlCommand oCommand = new SqlCommand("GetCurrentCurrencyExchangeRateByCurrencyId", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CurrencyId", pCurrencyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttCurrencyExchange = new DataTable();
                oAdapter.Fill(dttCurrencyExchange);
                if (dttCurrencyExchange != null && dttCurrencyExchange.Rows.Count > 0)
                {
                    return Convert.ToDecimal(dttCurrencyExchange.Rows[0]["ExchangeRate"]);
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable SearchProductServiceById(int pExpenseId)
        {
            try
            {

                SqlCommand oCommand = new SqlCommand("SearchProductServiceById", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@Id", pExpenseId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttProductServiceInfo = new DataTable();
                oAdapter.Fill(dttProductServiceInfo);
                
                    return dttProductServiceInfo;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable SearchSalesInvoiceById(int SalesInvoiceId)
        {
            try
            {

                SqlCommand oCommand = new SqlCommand("SearchSalesInvoiceById", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@SalesInvoiceId", SalesInvoiceId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttSalesInvoice = new DataTable();
                oAdapter.Fill(dttSalesInvoice);

                return dttSalesInvoice;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable AllCompanySalesInvoices(int pCompanyId)
        {
            try
            {

                SqlCommand oCommand = new SqlCommand("AllSalesInvoices", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttSalesInvoices = new DataTable();
                oAdapter.Fill(dttSalesInvoices);

                return dttSalesInvoices;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}

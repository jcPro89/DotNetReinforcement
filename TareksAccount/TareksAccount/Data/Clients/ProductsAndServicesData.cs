using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TareksAccount.Data.Clients
{
    class ProductsAndServicesData
    {
        public static DataTable AllProductsAndServices(int pCompanyId)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllProductsAndServices", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllProductsAndServices = new DataTable();
                oAdapter.Fill(dttAllProductsAndServices);
                return dttAllProductsAndServices;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static int AddProductService(string pName, string pDescription, decimal pExpenseCost, decimal pSuggestedPrice, int pCompanyId)
        {
            SqlTransaction oTransaction = null;
            try
            {

                SqlCommand oCommand = new SqlCommand("AddProductService", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;


                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Description", pDescription);
                oCommand.Parameters.AddWithValue("@ExpenseCost", pExpenseCost);
                oCommand.Parameters.AddWithValue("@SuggestedPrice", pSuggestedPrice);
                oCommand.Parameters.AddWithValue("@CompanyId", pCompanyId);

                Data.ConnectionData.ConnectToDataBase();
                oTransaction = Data.ConnectionData.oConnection.BeginTransaction();
                oCommand.Transaction = oTransaction;
                int oAffected = oCommand.ExecuteNonQuery();
                
                if (oAffected == 1)
                {
                    oTransaction.Commit();
                    Data.ConnectionData.DisconnectFromDatabase();
                    return 1;
                }
                else
                {
                    oTransaction.Rollback();
                    Data.ConnectionData.DisconnectFromDatabase();
                    return 0;
                }
                
            }
            catch (Exception ex)
            {
                if (oTransaction!=null)
                {
                    oTransaction.Rollback();
                }
                Data.ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }
    }
}

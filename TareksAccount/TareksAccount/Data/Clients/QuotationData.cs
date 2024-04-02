using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TareksAccount.Data.Clients
{
    class QuotationData
    {
        public static DataTable AllQuotationStatuses()
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllQuotationStatuses", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtStatuses = new DataTable();
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dtStatuses);
                return dtStatuses;

            }
            catch(Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }

        public static int GenerateNewQuotationNo (string pQuotationNo1)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("NextQuotationNo", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@QuotationNo1", pQuotationNo1);
                DataTable dtQuotationNo2 = new DataTable();
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dtQuotationNo2);
                if(dtQuotationNo2==null || dtQuotationNo2.Rows.Count == 0)
                {
                    return 1;
                }
                else
                {
                    return Convert.ToInt32(dtQuotationNo2.Rows[0]["QuotationNo2"]) + 1;
                }

            }
            catch (Exception ex)
            {
                Data.ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }

        public static int AddQuotation (int pStatusId, int pProjectId, int pCurrencyId, string pCustRef, string pQuotationNo1, string pQuotationNo2, DateTime pQuotationDate, string pShipType, string pBL_Reference, string pRemarks, string pAttention, bool pIsTaxable, DateTime pValidUntil, List<Shared_Entities.QuotationLine> quotationLines)
        {
            SqlTransaction oTransaction = null;
            try
            {
                SqlCommand oCommand = new SqlCommand("AddQuotation", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                
                oCommand.Parameters.AddWithValue("@StatusId", pStatusId);
                oCommand.Parameters.AddWithValue("@ProjectId", pProjectId);
                oCommand.Parameters.AddWithValue("@CurrencyId", pCurrencyId);
                oCommand.Parameters.AddWithValue("@CustRef", pCustRef);
                oCommand.Parameters.AddWithValue("@QuotationNo1", pQuotationNo1);
                oCommand.Parameters.AddWithValue("@QuotationNo2", GenerateNewQuotationNo(pQuotationNo1));
                oCommand.Parameters.AddWithValue("@QuotationDate", pQuotationDate);
                oCommand.Parameters.AddWithValue("@ShipType", pShipType);
                oCommand.Parameters.AddWithValue("@BL_Reference", pBL_Reference);
                oCommand.Parameters.AddWithValue("@Remarks", pRemarks);
                oCommand.Parameters.AddWithValue("@Attention", pAttention);
                oCommand.Parameters.AddWithValue("@IsTaxable", pIsTaxable);
                oCommand.Parameters.AddWithValue("@ValidUntil", pValidUntil);

                Data.ConnectionData.ConnectToDataBase();
                oTransaction = Data.ConnectionData.oConnection.BeginTransaction();
                oCommand.Transaction = oTransaction;
                int oAffected=oCommand.ExecuteNonQuery();
                if (oAffected == 1)
                {
                    //FIND LAST INSERTED ID OUT
                    oCommand = new SqlCommand("LastQuotationId", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Transaction = oTransaction;
                DataTable dtLastId = new DataTable();
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                oAdapter.Fill(dtLastId);
                int iLastId = Convert.ToInt32(dtLastId.Rows[0]["Id"]);
                
               
                    for (int i = 0; i < quotationLines.Count; i++)
                    {
                        oCommand = new SqlCommand("AddQuotationLine", Data.ConnectionData.oConnection);
                        oCommand.CommandType = CommandType.StoredProcedure;
                        oCommand.Transaction = oTransaction;
                        oCommand.Parameters.AddWithValue("@QuotationId", iLastId);
                        oCommand.Parameters.AddWithValue("@IsExpense", quotationLines[i].IsExpense);
                        oCommand.Parameters.AddWithValue("@IsProductService", quotationLines[i].IsProductService);
                        oCommand.Parameters.AddWithValue("@ItemId", quotationLines[i].ItemId);
                        oCommand.Parameters.AddWithValue("@Date", quotationLines[i].Date);
                        oCommand.Parameters.AddWithValue("@Incharge", quotationLines[i].Incharge);
                        oCommand.Parameters.AddWithValue("@CurrencyId", quotationLines[i].CurrencyId);
                        oCommand.Parameters.AddWithValue("@FC_Amount", quotationLines[i].FC_Amount);
                        oCommand.Parameters.AddWithValue("@LC_Amount", quotationLines[i].LC_Amount);
                        oCommand.Parameters.AddWithValue("@ClientCharge", quotationLines[i].ClientCharge);                                                                     
                        oCommand.ExecuteNonQuery();
                    }

                    //oCommand.CommandText = "AddQuotationLine";
                    //oCommand.Parameters.Clear();
                    //SqlParameter oQuotationId = new SqlParameter("@QuotationId",SqlDbType.Int);
                    //SqlParameter oIsExpense = new SqlParameter("@IsExpense", SqlDbType.Bit);
                    //SqlParameter oIsProductService = new SqlParameter("@IsProductService", SqlDbType.Bit);
                    //SqlParameter oItemId = new SqlParameter("@ItemId", SqlDbType.Int);
                    //SqlParameter oDate= new SqlParameter("@Date", SqlDbType.Date);
                    //SqlParameter oInchargeId = new SqlParameter("@Incharge", SqlDbType.Int);
                    //SqlParameter oCurrencyId = new SqlParameter("@CurrencyId", SqlDbType.Int);
                    //SqlParameter oFC_Amount = new SqlParameter("@FC_Amount", SqlDbType.Money);
                    //SqlParameter oLC_Amount = new SqlParameter("@LC_Amount", SqlDbType.Money);
                    //SqlParameter oClientCharge = new SqlParameter("@ClientCharge", SqlDbType.Money);
                    
                }
                
                oTransaction.Commit();
                Data.ConnectionData.DisconnectFromDatabase();
                return 1;
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                Data.ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }
    }
}

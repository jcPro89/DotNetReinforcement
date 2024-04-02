using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TareksAccount.Data
{
    class ConnectionData
    {
        private static string sConnectionString = "Server=localhost\\SQLEXPRESS;Database=Tarek_Test;Integrated Security=SSPI";
        public static SqlConnection oConnection = new SqlConnection(sConnectionString);

        public static DataTable LoginData(string pUserName)
        {
            try
            {

                SqlCommand oCommand = new SqlCommand("LoginDataByUsername", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@Username", pUserName);

                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttLoginData = new DataTable();
                oAdapter.Fill(dttLoginData);

                return dttLoginData;
            
            }
            catch (SqlException exSQL)
            {
                throw exSQL;
            }
        }

        public static void ConnectToDataBase()
        {            
            if(oConnection.State==ConnectionState.Closed)
            oConnection.Open();
        }

        public static void DisconnectFromDatabase()
        {
            if(oConnection.State!=ConnectionState.Closed)
            oConnection.Close();
        }
    }
}

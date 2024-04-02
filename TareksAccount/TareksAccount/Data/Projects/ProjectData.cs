using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TareksAccount.Data.Projects
{
    class ProjectData
    {
        public static DataTable AllProjects(int pCompanyId)
        {
            try
            {
                SqlCommand oCommand = new SqlCommand("AllProjects", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("CompanyId", pCompanyId);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oCommand);
                DataTable dttAllProjects = new DataTable();
                oAdapter.Fill(dttAllProjects);
                return dttAllProjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int AddProject(int pClientId, string pName, string pDescription)
        {
            
            try
            {

                SqlCommand oCommand = new SqlCommand("AddProject", Data.ConnectionData.oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;


                oCommand.Parameters.AddWithValue("@ClientId", pClientId);
                oCommand.Parameters.AddWithValue("@Name", pName);
                oCommand.Parameters.AddWithValue("@Description", pDescription);
                
                Data.ConnectionData.ConnectToDataBase();
                               
                int oAffected = oCommand.ExecuteNonQuery();

                if (oAffected == 1)
                {
Data.ConnectionData.DisconnectFromDatabase();
                    return 1;
                }
                else
                {
                    Data.ConnectionData.DisconnectFromDatabase();
                    return 0;
                }

            }
            catch (Exception ex)
            {
Data.ConnectionData.DisconnectFromDatabase();
                throw ex;
            }
        }

    }
}

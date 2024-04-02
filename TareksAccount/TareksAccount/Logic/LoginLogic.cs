using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TareksAccount.Logic
{
    class LoginLogic
    {
        public static DataTable LoginData(string pUserName)
        {
            return Data.ConnectionData.LoginData(pUserName);
        }
    }
}

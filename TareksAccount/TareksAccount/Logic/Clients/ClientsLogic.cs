using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TareksAccount.Logic.Clients
{
    class ClientsLogic
    {
        public static DataTable AllClientGroups(int pCompanyId)
        {
            return Data.Clients.ClientsData.AllClientGroups(pCompanyId);
        }
        public static int AddNewClientsGroup(string pName, string pDescription, int pCompanyId)
        {
            return Data.Clients.ClientsData.AddNewClientsGroup(pName, pDescription, pCompanyId);
        }

        public static DataTable AllClients(int pCompanyid)
        {
            return Data.Clients.ClientsData.AllClients(pCompanyid);
        }

        public static int AddClient(int pGroupId, int pCurrencyId, string pSalesRep, string pTermsOfPayment, string pName, string pCountry, string pCity, string pAddress, string pCompanyName, string pPhone, string pAltPhone, string pEmail, string pCC, double pCreditLimit, int pCreditDays, string pSourceOfClient, string pNotes, bool pStatus, DateTime pClientSince)
        {
            return Data.Clients.ClientsData.AddClient(pGroupId, pCurrencyId, pSalesRep, pTermsOfPayment, pName, pCountry, pCity, pAddress, pCompanyName, pPhone, pAltPhone, pEmail, pCC, pCreditLimit, pCreditDays, pSourceOfClient, pNotes, pStatus, pClientSince);
        }
        }
}

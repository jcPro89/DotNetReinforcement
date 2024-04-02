using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareksAccount.Logic.Clients
{
    class QuotationLogic
    {
        public static DataTable AllQuotationStatuses()
        {
            return Data.Clients.QuotationData.AllQuotationStatuses();
        }

        public static int AddQuotation(int pStatusId, int pProjectId, int pCurrencyId, string pCustRef, string pQuotationNo1, string pQuotationNo2, DateTime pQuotationDate, string pShipType, string pBL_Reference, string pRemarks, string pAttention, bool pIsTaxable, DateTime pValidUntil, List<Shared_Entities.QuotationLine> quotationLines)
        {
            return Data.Clients.QuotationData.AddQuotation(pStatusId, pProjectId, pCurrencyId, pCustRef, pQuotationNo1, pQuotationNo2, pQuotationDate, pShipType, pBL_Reference, pRemarks, pAttention, pIsTaxable, pValidUntil, quotationLines);
        }

        public static int GenerateNewQuotationNo(string pQuotationNo1)
        {
            return Data.Clients.QuotationData.GenerateNewQuotationNo(pQuotationNo1);
        }
        }
}

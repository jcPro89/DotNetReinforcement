using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TareksAccount.Logic.Settings
{
    class SettingsLogic
    {

        public static DataTable AllCurrencies()
        {
            return Data.Settings.SettingsData.AllCurrencies();
        }
        public static DataTable AllCurrenciesCurrentExchangeRate()
        {
            return Data.Settings.SettingsData.AllCurrenciesCurrentExchangeRate();
        }

        public static DataTable LocalCurrency()
        {
            return Data.Settings.SettingsData.LocalCurrency();
        }
        public static int AddCurrency(string pCode, string pName, decimal pExchangeRate, DateTime pDateFrom, DateTime pDateTo)
        {
            return Data.Settings.SettingsData.AddCurrency(pCode, pName, pExchangeRate, pDateFrom, pDateTo);
        }


        public static DataTable AllCurrenciesExchangeRateHistory()
        {
            return Data.Settings.SettingsData.AllCurrenciesExchangeRateHistory();
        }

        public static int UpdateCompanyDetails(int pCompanyId, string pName, byte[] pLogo, string pPhone, string pEmail, string pWebsite, string pCode, string pRegistrationNo, string pVAT_Number, string pLegalName, string pLegalAddress)
        {
            return Data.Settings.SettingsData.UpdateCompanyDetails(pCompanyId, pName, pLogo, pPhone, pEmail, pWebsite, pCode, pRegistrationNo, pVAT_Number, pLegalName, pLegalAddress);
        }

        public static DataTable GetCompanyDetails(int pCompanyId)
        {
            return Data.Settings.SettingsData.GetCompanyDetails(pCompanyId);
        }

        public static DataTable AllCompanies()
        {
            return Data.Settings.SettingsData.AllCompanies();
        }

        public static int AddCompany(string pName, byte[] pLogo, string pPhone, string pEmail, string pWebsite, string pCode, string pRegistrationNo, string pVAT_Number, string pLegalName, string pLegalAddress)
        {
            return Data.Settings.SettingsData.AddCompany(pName, pLogo, pPhone, pEmail, pWebsite, pCode, pRegistrationNo, pVAT_Number, pLegalName, pLegalAddress);


        }

        public static int AddCurrencyExchangeRate(int pCurrencyId, decimal pExchangeRate, DateTime pDateFrom, DateTime pDateTo)
        {
            return Data.Settings.SettingsData.AddCurrencyExchangeRate(pCurrencyId, pExchangeRate, pDateFrom, pDateTo);
        }

        public static int ModifyCurrencyExchangeRate(int pExchangeRateId, decimal pExchangeRate, DateTime pDateFrom, DateTime pDateTo)
        {
            return Data.Settings.SettingsData.ModifyCurrencyExchangeRate(pExchangeRateId, pExchangeRate, pDateFrom, pDateTo);
        }


    }
}
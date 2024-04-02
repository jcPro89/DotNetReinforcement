using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TareksAccount.Logic.Clients
{
    class ProductsAndServicesLogic
    {
        public static DataTable AllProductsAndServices(int pCompanyId)
        {
            return Data.Clients.ProductsAndServicesData.AllProductsAndServices(pCompanyId);
        }

        public static int AddProductService(string pName, string pDescription, decimal pExpenseCost, decimal pSuggestedPrice, int pCompanyId)
        {
            return Data.Clients.ProductsAndServicesData.AddProductService(pName, pDescription, pExpenseCost, pSuggestedPrice, pCompanyId);
        }
        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareksAccount.Shared_Entities
{
    public class ProductService
    {
        private int _id;
        private string _name;
        private string _description;
        private decimal _expenseCost;
        private decimal _suggestedPrice;

        public ProductService (int Id, string Name, decimal ExpenseCost, decimal SuggestedPrice)
        {
            _id = Id;
            _name = Name;
            _expenseCost = ExpenseCost;
            _suggestedPrice = SuggestedPrice;
        }

        public int Id
        {
            get { return _id; }
            set { _id = Id; }

        }

        public string Name
        {
            get { return _name; }
            set { _name = Name; }

        }

        public string Description
        {
            get { return _description; }
            set { _description = Description; }
        }

        public decimal ExpenseCost
        {
            get { return _expenseCost; }
            set { _expenseCost = ExpenseCost; }
        }

        public decimal SuggestedPrice
        {
            get { return _suggestedPrice; }
            set { _suggestedPrice = SuggestedPrice; }

        }
    }
}

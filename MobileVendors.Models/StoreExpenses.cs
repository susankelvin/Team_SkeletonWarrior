using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.Models
{
    public class StoreExpenses
    {
        public string StoreName { get; set; }

        public List<KeyValuePair<String, String>> ExpensesPerMonth { get; set; }

        public StoreExpenses()
        {
            this.ExpensesPerMonth = new List<KeyValuePair<string, string>>();
        }
    }
}

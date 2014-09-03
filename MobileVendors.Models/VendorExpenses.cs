using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.Models
{
    public class VendorExpenses
    {
        public string VendorName { get; set; }

        public List<KeyValuePair<String, String>> ExpensesPerMonth { get; set; }

        public VendorExpenses()
        {
            this.ExpensesPerMonth = new List<KeyValuePair<string, string>>();
        }
    }
}

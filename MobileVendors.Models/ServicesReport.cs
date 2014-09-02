using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.Models
{
    public class ServicesReport
    {
        public String ServiceName { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalSum { get; set; }
    }
}

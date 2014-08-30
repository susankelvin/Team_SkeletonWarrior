namespace MobileVendors.ConsoleClient
{
    using System;
    using System.Linq;
    using MobileVendors.Data;
    using MobileVendors.Models;

    internal class ConsoleClient
    {
        private static void Main(string[] args)
        {
            var sampleContext = new MobileVendorsData();
            sampleContext.Vendors.Add(new Vendor()
            {
                VendorName = "Mtel",
                PhoneNumber = "+359888123123",
                Email = "pesho@mtel.com"
            });

            sampleContext.SaveChanges();
        }
    }
}
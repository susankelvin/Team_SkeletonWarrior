namespace MobileVendors.ConsoleClient
{
    using System;
    using System.Linq;
    using MobileVendors.Data;
    using MobileVendors.Models;

    internal class ConsoleClient
    {
        private static void Main()
        {
            CreateJsonReports();            
        }

        private static void CreateJsonReports()
        {
            var context = new MobileVendorsDbContext();
            JsonReportCreator jrc = new JsonReportCreator(context);
            jrc.CreateReport();
        }
    }
}
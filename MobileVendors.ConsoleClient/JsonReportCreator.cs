namespace MobileVendors.ConsoleClient
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    using MobileVendors.Data;
    using MobileVendors.Models;
    using System.IO;

    public class JsonReportCreator
    {
        private readonly IMobileVendorsData data;

        public JsonReportCreator() : this(new MobileVendorsData())
        {
        }

        public JsonReportCreator(IMobileVendorsData data)
        {
            this.data = data;
        }

        public void CreateReport()
        {            
            var subscriptions = this.data.Subscriptions.All()
                .Select(s =>
                new
                {
                    Id = s.Id,
                    ProductName = s.Service.ServiceName,
                    VendorName = s.Store.Vendor.VendorName,
                    TotalIncome = s.TotalIncome,
                    Quantity = s.Quantity
                })
                .GroupBy(s => s.ProductName)                
                .ToList();

            foreach (var sub in subscriptions)
            {
                var first = sub.First();
                JObject product = new JObject();
                product["product-id"] = first.Id;
                product["product-name"] = first.ProductName;
                product["vendor-name"] = first.VendorName;


                int totalQuantity = 0;
                decimal totalIncome = 0;

                foreach (var item in sub)
                {
                    totalQuantity += item.Quantity;
                    totalIncome += item.TotalIncome;
                }

                product["total-quantity-sold"] = totalQuantity;
                product["total-incomes"] = totalIncome;
                
                WriteReportToFile(product.ToString(), first.Id.ToString());
            }
        }

        public void WriteReportToFile(string json, string id)
        {
            string filePath = @"..\..\Json-Reports\" + id + ".json";
            File.WriteAllText(filePath, json);
        }
    }
}

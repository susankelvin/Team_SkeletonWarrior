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
            // Run only once
            PopullateDatabase();

            CreateJsonReports();            
        }

        private static void CreateJsonReports()
        {
            var context = new MobileVendorsDbContext();
            JsonReportCreator jrc = new JsonReportCreator(context);
            jrc.CreateReport();
        }

        private static void PopullateDatabase()
        {
            var context = new MobileVendorsData();

            var popullator = new SQLPopullator(context);
            popullator.PopullateCategory("Mobile services");
            popullator.PopullateCategory("Internet services");
            popullator.PopullateCategory("Mobile and internet services");
            popullator.PopullateCategory("TV");
            popullator.SaveChanges();

            var mobileCategory = context.Categories.All().First(x => x.Description == "Mobile services").Id;
            var internetCategory = context.Categories.All().First(x => x.Description == "Internet services").Id;
            var mobileAndInternatCategory = context.Categories.All().First(x => x.Description == "Mobile and internet services").Id;
            var tv = context.Categories.All().First(x => x.Description == "TV").Id;

            popullator.PopullateService("Classic XS", 9.9m, mobileCategory);
            popullator.PopullateService("Classic S", 19.9m, mobileCategory);
            popullator.PopullateService("Classic M", 29.9m, mobileCategory);
            popullator.PopullateService("Classic L", 39.9m, mobileCategory);
            popullator.PopullateService("Classic XL", 49.9m, mobileCategory);
            popullator.PopullateService("Smart XS", 12.9m, mobileCategory);
            popullator.PopullateService("Smart S", 16.9m, mobileCategory);
            popullator.PopullateService("Smart M", 24.9m, mobileCategory);
            popullator.PopullateService("Smart L", 39.9m, mobileCategory);
            popullator.PopullateService("Smart XL", 59.9m, mobileCategory);
            popullator.PopullateService("Web and talk XS", 6.99m, mobileAndInternatCategory);
            popullator.PopullateService("Web and talk S", 13.99m, mobileAndInternatCategory);
            popullator.PopullateService("Web and talk M", 16.99m, mobileAndInternatCategory);
            popullator.PopullateService("Web and talk L", 20.99m, mobileAndInternatCategory);
            popullator.PopullateService("Web and talk XL", 29.99m, mobileAndInternatCategory);
            popullator.PopullateService("Go web Basic", 2.90m, internetCategory);
            popullator.PopullateService("Go web Advanced", 5.90m, internetCategory);
            popullator.PopullateService("Go web Pro", 8.90m, internetCategory);
            popullator.PopullateService("Go web Ultimate", 13.90m, internetCategory);
            popullator.PopullateService("Go web Enterprise", 34.90m, internetCategory);
            popullator.PopullateService("Economic", 7.80m, tv);
            popullator.PopullateService("Standard", 12.80m, tv);
            popullator.PopullateService("Premium", 14.80m, tv);
            popullator.PopullateService("Deluxe", 19.80m, tv);
            popullator.PopullateService("Deluxe+", 22.80m, tv);
            popullator.SaveChanges();

            popullator.PopullateTown("Sofia");
            popullator.PopullateTown("Varna");
            popullator.PopullateTown("Plovdiv");            
            popullator.PopullateTown("Bourgas");            
            popullator.PopullateTown("Pleven");            
            popullator.PopullateTown("Stara Zagora");
            popullator.SaveChanges();

            popullator.PopullateVendor("MednaTel");
            popullator.PopullateVendor("Globalizator");           
            popullator.PopullateVendor("BOB s nadenitza");            
            popullator.PopullateVendor("Megalag");
            popullator.SaveChanges();

            PoppulateStores(popullator, 1);
            PoppulateStores(popullator, 2);
            PoppulateStores(popullator, 3);
            PoppulateStores(popullator, 4);
            popullator.SaveChanges();

            popullator.PopullateSubsrciption(2, DateTime.Now, 2, 12.9m, 1, 1);
            popullator.PopullateSubsrciption(2, DateTime.Now, 2, 12.9m, 1, 2);
            popullator.PopullateSubsrciption(2, DateTime.Now, 2, 15.9m, 1, 3);
            popullator.PopullateSubsrciption(2, DateTime.Now, 1, 15.9m, 1, 4);
            popullator.PopullateSubsrciption(2, DateTime.Now, 2, 15.9m, 1, 5);
            popullator.PopullateSubsrciption(2, DateTime.Now, 2, 15.9m, 1, 6);
            popullator.PopullateSubsrciption(2, DateTime.Now, 2, 15.9m, 1, 7);
            popullator.PopullateSubsrciption(2, DateTime.Now, 2, 15.9m, 1, 8);
            popullator.PopullateSubsrciption(2, DateTime.Now, 1, 19.9m, 1, 9);
            popullator.PopullateSubsrciption(2, DateTime.Now, 1, 19.9m, 1, 10);
            popullator.PopullateSubsrciption(2, DateTime.Now, 1, 19.9m, 1, 11);
            popullator.PopullateSubsrciption(2, DateTime.Now, 1, 19.9m, 1, 12);
            popullator.PopullateSubsrciption(2, DateTime.Now, 1, 22.9m, 1, 13);
            popullator.PopullateSubsrciption(2, DateTime.Now, 1, 22.9m, 1, 14);

            popullator.SaveChanges();
        }
 
        private static void PoppulateStores(SQLPopullator popullator, int vendorId)
        {
            popullator.PopullateStore("Center", vendorId, 1);
            popullator.PopullateStore("Liulin", vendorId, 1);
            popullator.PopullateStore("Nadezhda", vendorId, 1);
            popullator.PopullateStore("Mladost", vendorId, 1);
            popullator.PopullateStore("Center", vendorId, 3);            
            popullator.PopullateStore("Iztochen", vendorId, 3);
            popullator.PopullateStore("Zapaden", vendorId, 3);
            popullator.PopullateStore("Center", vendorId, 2);
            popullator.PopullateStore("Iztochen", vendorId, 2);
            popullator.PopullateStore("Zapaden", vendorId, 2);
            popullator.PopullateStore("Center", vendorId, 4);
            popullator.PopullateStore("Iztochen", vendorId, 4);
            popullator.PopullateStore("Zapaden", vendorId, 4);
            popullator.PopullateStore("Center", vendorId, 5);
            popullator.PopullateStore("Iztochen", vendorId, 5);
            popullator.PopullateStore("Zapaden", vendorId, 5);
            popullator.PopullateStore("Center", vendorId, 6);
            popullator.PopullateStore("Iztochen", vendorId, 6);
            popullator.PopullateStore("Zapaden", vendorId, 6);
        }
    }
}
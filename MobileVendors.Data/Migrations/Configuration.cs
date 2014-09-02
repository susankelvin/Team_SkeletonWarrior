namespace MobileVendors.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity.Migrations;

    using MobileVendors.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<MobileVendorsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "MobileVendors.Data.MobileVendorsDbContext";
        }
    }
}
namespace MobileVendors.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MobileVendors.Data;
    using MobileVendors.Models;

    public sealed class Configuration : DbMigrationsConfiguration<MobileVendorsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MobileVendors.Data.MobileVendorsDbContext";
        }

        protected override void Seed(MobileVendorsDbContext context)
        {
            this.SeedCourses(context);
            this.SeedStudents(context);
            this.SeedHomeworks(context);
        }

        private void SeedHomeworks(MobileVendorsDbContext context)
        {
        }

        private void SeedStudents(MobileVendorsDbContext context)
        {
        }

        private void SeedCourses(MobileVendorsDbContext context)
        {
        }
    }
}
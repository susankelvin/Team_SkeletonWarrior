namespace MobileVendors.Controllers
{
    using System.Collections.Generic;
    using System.Data.SQLite;
    using MobileVendors.Models;
    
    public class SQLiteController
    {
        private readonly SQLiteConnection connection;

        public SQLiteController()
        {
            string connectionString =
                "Data Source=..\\..\\Taxes.s3db;Version=3;New=True;Compress=True;";
            this.connection = new SQLiteConnection(connectionString);
        }

        public ICollection<ServiceTax> GetServicesTaxes()
        {
            var taxes = new HashSet<ServiceTax>();
            var command = new SQLiteCommand("SELECT Id,ServiceName,ServiceTax FROM Taxes", this.connection);
            this.connection.Open();

            var reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    var tax = new ServiceTax()
                    {
                        Id = (dynamic)reader["Id"],
                        ServiceName = (dynamic)reader["ServiceName"],
                        Tax = (dynamic)reader["ServiceTax"]
                    };
                    taxes.Add(tax);
                }
            }

            this.connection.Close();

            return taxes;
        }
    }
}
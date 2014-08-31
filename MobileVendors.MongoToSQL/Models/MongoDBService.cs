using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.MongoToSQL.Models
{
    public class MongoDBService
    {
        public ObjectId Id { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        public MongoDBCategory Category { get; set; }

        public MongoDBService()
        {
            Category = new MongoDBCategory();
        }
    }
}

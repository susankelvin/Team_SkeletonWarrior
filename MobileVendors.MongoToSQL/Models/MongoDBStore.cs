using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.MongoToSQL.Models
{
    public class MongoDBStore
    {
        public ObjectId Id { get; set; }

        public string Address { get; set; }

        public MongoDBVendor Vendor { get; set; }

        public MongoDBTown Town { get; set; }

        public MongoDBStore()
        {
            Vendor = new MongoDBVendor();
            Town = new MongoDBTown();
        }
    }
}

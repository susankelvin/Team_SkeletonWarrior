using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.MongoToSQL.Models
{
    public class MongoDBCategory
    {
        public ObjectId Id { get; set; }

        public string Description { get; set; }
    }
}

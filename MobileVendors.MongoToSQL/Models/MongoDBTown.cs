using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileVendors.MongoToSQL.Models
{
    public class MongoDBTown
    {
        public ObjectId Id { get; set; }

        public string TownName { get; set; }
    }
}

namespace MobileVendors.Models.MongoDBModels
{
    using MongoDB.Bson;

    public class MongoDBStore
    {
        public ObjectId Id { get; set; }

        public string Address { get; set; }

        public MobileVendors.Models.MongoDBModels.MongoDBVendor Vendor { get; set; }

        public MongoDBTown Town { get; set; }

        public MongoDBStore()
        {
            Vendor = new MobileVendors.Models.MongoDBModels.MongoDBVendor();
            Town = new MongoDBTown();
        }
    }
}
namespace MobileVendors.Models.MongoDBModels
{
    using MongoDB.Bson;

    public class MongoDBVendor
    {
        public ObjectId Id { get; set; }

        public string VendorName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
namespace MobileVendors.Models.MongoDBModels
{
    using MongoDB.Bson;

    public class MongoDBCategory
    {
        public ObjectId Id { get; set; }

        public string Description { get; set; }
    }
}
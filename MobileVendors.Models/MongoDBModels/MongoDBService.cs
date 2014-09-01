namespace MobileVendors.Models.MongoDBModels
{
    using MongoDB.Bson;

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
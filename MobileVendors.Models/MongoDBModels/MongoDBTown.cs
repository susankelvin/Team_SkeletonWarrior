namespace MobileVendors.Models.MongoDBModels
{
    using MongoDB.Bson;

    public class MongoDBTown
    {
        public ObjectId Id { get; set; }

        public string TownName { get; set; }
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Operations.Core.Serialization;

namespace Operations.Core.Entities
{
    public class Operation  
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int AccountId { get; set; }
        public DateTime Date {  get; set; } = DateTime.UtcNow;
        [BsonSerializer(typeof(CustomDictionarySerializer))]
        public Dictionary<string, object> Description { get; set; }
    }
}

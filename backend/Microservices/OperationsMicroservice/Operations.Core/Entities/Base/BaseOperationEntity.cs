using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Operations.Core.Entities.Base
{
    public class BaseOperationEntity
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int AccountId { get; set; }
    }
}

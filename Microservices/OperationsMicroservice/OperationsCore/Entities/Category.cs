using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Operations.Core.Entities
{
    public class Category : BaseEntity
    {    
        [BsonElement("CategoryType")]
        public MongoDBRef CategoryType { get; set; }
    }
}

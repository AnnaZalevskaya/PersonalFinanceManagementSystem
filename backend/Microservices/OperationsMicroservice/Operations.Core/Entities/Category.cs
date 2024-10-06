using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Operations.Core.Entities.Base;

namespace Operations.Core.Entities
{
    public class Category : BaseEntity
    {    
        [BsonElement("CategoryType")]
        public MongoDBRef? CategoryType { get; set; }
    }
}

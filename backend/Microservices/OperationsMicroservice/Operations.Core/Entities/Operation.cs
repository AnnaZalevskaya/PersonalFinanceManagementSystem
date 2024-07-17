using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Operations.Core.Entities.Base;
using Operations.Core.Serialization;

namespace Operations.Core.Entities
{
    public class Operation : BaseOperationEntity
    {
        public DateTime Date {  get; set; } = DateTime.UtcNow.AddHours(3);
        [BsonSerializer(typeof(CustomDictionarySerializer))]
        public Dictionary<string, object> Description { get; set; }
    }
}

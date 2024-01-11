using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using System.Text.Json;
using MongoDB.Bson.IO;
using MongoDB.Bson;

namespace Operations.Core.Serialization
{
    public class CustomDictionarySerializer : SerializerBase<Dictionary<string, object>>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, 
            Dictionary<string, object> value)
        {
            var bsonWriter = context.Writer;
            bsonWriter.WriteStartDocument();

            foreach (var pair in value)
            {
                bsonWriter.WriteName(pair.Key);
                SerializeValue(context, args, pair.Value);
            }

            bsonWriter.WriteEndDocument();
        }

        private void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var bsonWriter = context.Writer;

            if (value == null)
            {
                bsonWriter.WriteNull();
            }

            else if (value is int intValue)
            {
                bsonWriter.WriteInt32(intValue);
            }

            else if (value is double doubleValue)
            {
                bsonWriter.WriteDouble(doubleValue);
            }

            else if (value is JsonElement jsonElement)
            {
                var jsonString = jsonElement.GetRawText();
                bsonWriter.WriteString(jsonString);
            }
        }

        public override Dictionary<string, object> Deserialize(BsonDeserializationContext context, 
            BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            bsonReader.ReadStartDocument();
            var result = new Dictionary<string, object>();

            while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
            {
                var key = bsonReader.ReadName();
                var value = DeserializeValue(context, args);
                result.Add(key, value);
            }

            bsonReader.ReadEndDocument();

            return result;
        }

        private object DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            switch (bsonReader.GetCurrentBsonType())
            {
                case (BsonType.Int32):
                    return bsonReader.ReadInt32();
                    break;

                case (BsonType.Double): 
                    return bsonReader.ReadDouble();
                    break;

                case (BsonType.String): 
                    return bsonReader.ReadString();
                    break;

                case (BsonType.DateTime): 
                    return bsonReader.ReadDateTime();
                    break;

                case (BsonType.Document): 
                    return bsonReader.ReadString();
                    break;

                default:
                    throw new BsonSerializationException("Unexpected BsonType" + bsonReader.GetCurrentBsonType());
            }
        }
    }
}

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
                SerializeValue(context, pair.Value);
            }

            bsonWriter.WriteEndDocument();
        }

        private void SerializeValue(BsonSerializationContext context, object value)
        {
            var bsonWriter = context.Writer;

            if (value == null)
            {
                bsonWriter.WriteNull();
            }

            else if (value is JsonElement jsonElement)
            {
                var jsonString = jsonElement.GetRawText().Replace("\"", "");

                if (int.TryParse(jsonString, out int intVal))
                {
                    bsonWriter.WriteInt32(intVal);
                }

                else if (double.TryParse(jsonString, out double doubleVal))
                {
                    bsonWriter.WriteDouble(doubleVal);
                }

                else
                {
                    bsonWriter.WriteString(jsonString);
                }
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

                case (BsonType.Double): 
                    return bsonReader.ReadDouble();

                case (BsonType.String): 
                    return bsonReader.ReadString();

                case (BsonType.DateTime): 
                    return bsonReader.ReadDateTime();

                case (BsonType.Document): 
                    return bsonReader.ReadString();

                default:
                    throw new BsonSerializationException("Unexpected BsonType" + bsonReader.GetCurrentBsonType());
            }
        }
    }
}

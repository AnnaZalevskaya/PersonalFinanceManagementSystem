using MongoDB.Driver;

namespace Operations.Core.Extensions
{
    public static class MongoDbExtension
    {
        public static string GetAsName<T>(this MongoDBRef mongoDBRef)
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase(mongoDBRef.DatabaseName);
            var collection = database.GetCollection<T>(mongoDBRef.CollectionName); 
            var filter = Builders<T>.Filter.Eq("_id", mongoDBRef.Id); 
            var result = collection.Find(filter).FirstOrDefault(); 

            if (result != null)
            {
                var nameProperty = result.GetType().GetProperty("Name"); 

                if (nameProperty != null)
                {
                    return nameProperty.GetValue(result).ToString();
                }
            }

            return null; 
        }
    }
}

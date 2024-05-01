using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Operations.Core.Entities;
using Operations.Infrastructure.Settings;

namespace Operations.Infrastructure.Data
{
    public class OperationsDbContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }
        public IMongoCollection<Category> Categories { get; }
        public IMongoCollection<CategoryType> CategoryTypes { get; }
        public IMongoCollection<Operation> Operations { get; }

        public OperationsDbContext(IOptions<DatabaseSettings> dbOptions)
        {
            var dbSettings = dbOptions.Value;
            var client = new MongoClient(dbSettings.ConnectionString);
            Database = client.GetDatabase(dbSettings.DatabaseName);
            Categories = Database.GetCollection<Category>(dbSettings.CategoriesCollectionName);
            CategoryTypes = Database.GetCollection<CategoryType>(dbSettings.CategoryTypesCollectionName);
            Operations = Database.GetCollection<Operation>(dbSettings.OperationsCollectionName);
            Client = client;
        }
    }
}

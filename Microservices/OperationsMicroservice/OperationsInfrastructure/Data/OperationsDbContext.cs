using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Operations.Core.Entities;
using Operations.Infrastructure.Settings;

namespace Operations.Infrastructure.Data
{
    public class OperationsDbContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Category> _categories;
        private readonly IMongoCollection<CategoryType> _categoryTypes;
        private readonly IMongoCollection<Operation> _operations;

        public OperationsDbContext(IOptions<DatabaseSettings> dbOptions)
        {
            var dbSettings = dbOptions.Value;
            _client = new MongoClient(dbSettings.ConnectionString);
            _database = _client.GetDatabase(dbSettings.DatabaseName);
            _categories = _database.GetCollection<Category>(dbSettings.CategoriesCollectionName);
            _categoryTypes = _database.GetCollection<CategoryType>(dbSettings.CategoryTypesCollectionName);
            _operations = _database.GetCollection<Operation>(dbSettings.OperationsCollectionName);
        }

        public IMongoClient Client
        {
            get
            {
                return _client;
            }
        }

        public IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }

        public IMongoCollection<Category> Categories
        {
            get
            {
                return _categories;
            }
        }

        public IMongoCollection<CategoryType> CategoryTypes
        {
            get
            {
                return _categoryTypes;
            }
        }

        public IMongoCollection<Operation> Operations
        {
            get
            {
                return _operations;
            }
        }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using Operations.Application.Interfaces.gRPC;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;

namespace Operations.Infrastructure.Repositories.gRPC
{
    public class OperationsGrpcRepository : IOperationsGrpcRepository
    {
        private readonly OperationsDbContext _context;

        public OperationsGrpcRepository(OperationsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AggregationResult>> GetByAccountIdAsync(int accountId)
        {
            var matchStage = new BsonDocument("$match", new BsonDocument("AccountId", accountId));

            var lookupStage = new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "category" },
                { "localField", "Description.CategoryId" },
                { "foreignField", "_id" },
                { "as", "category" }
            });

            var unwindStage1 = new BsonDocument("$unwind", "$category");

            var lookupStage2 = new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "category_type" },
                { "localField", "category.CategoryType.$id" },
                { "foreignField", "_id" },
                { "as", "categoryType" }
            });

            var unwindStage2 = new BsonDocument("$unwind", "$categoryType");

            var groupStage = new BsonDocument("$group", new BsonDocument
            {
                { "_id", "$categoryType.Name" },
                { "totalAmount", new BsonDocument("$sum", "$Description.Amount") }
            });

            var pipeline = new[] { matchStage, lookupStage, unwindStage1, lookupStage2, unwindStage2, groupStage };

            var result = await _context.Operations
                .Aggregate<AggregationResult>(pipeline)
                .ToListAsync();

            foreach( var item in result )
            {
                Console.WriteLine(item._id);
                Console.WriteLine(item.totalAmount);
                Console.WriteLine("---------------------------");
            }

            return result;
        }
    }
}

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

        public async Task<IEnumerable<AggregationResult>> GetByAccountIdAsync(int accountId, CancellationToken cancellationToken)
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

            var projectStage = new BsonDocument("$project", new BsonDocument
            {
                { "operationCategoryType", "$_id" }, 
                { "totalAmount", 1 },
                { "_id", 0 }
            });

            var pipeline = new[] { matchStage, lookupStage, unwindStage1, lookupStage2, unwindStage2, groupStage, projectStage };

            var result = await _context.Operations
                .Aggregate<AggregationResult>(pipeline)
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<AggregationResult>> GetByAccountIdByPeriodAsync(int accountId, string start_date, string end_date, 
            CancellationToken cancellationToken)
        {
            DateTime startDate = DateTime.Parse(start_date);
            DateTime endDate = DateTime.Parse(end_date);

            var matchStage = new BsonDocument("$match", new BsonDocument
            {
                { "AccountId", accountId },
                { "Date", new BsonDocument{
                    { "$gt", startDate },
                    { "$lt", endDate }
                }}
            });

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

            var projectStage = new BsonDocument("$project", new BsonDocument
            {
                { "operationCategoryType", "$_id" },
                { "totalAmount", 1 },
                { "_id", 0 }
            });

            var pipeline = new[] { matchStage, lookupStage, unwindStage1, lookupStage2, unwindStage2, groupStage, projectStage };

            var result = await _context.Operations
                .Aggregate<AggregationResult>(pipeline)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}

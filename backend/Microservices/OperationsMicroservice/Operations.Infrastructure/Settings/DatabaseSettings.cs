namespace Operations.Infrastructure.Settings
{
    public class DatabaseSettings
    {
        public string? CategoriesCollectionName { get; init; }
        public string? CategoryTypesCollectionName { get; init; }
        public string? OperationsCollectionName { get; init; }
        public string? RecurringPaymentsName { get; init; }
        public string? ConnectionString { get; init; }
        public string? DatabaseName { get; init; }
    }
}

namespace Operations.Application.Settings
{
    public class AzureBlobStorageSettings
    {
        public string? StorageAccountConnection { get; init; }
        public string? ContainerName { get; init; }
    }
}

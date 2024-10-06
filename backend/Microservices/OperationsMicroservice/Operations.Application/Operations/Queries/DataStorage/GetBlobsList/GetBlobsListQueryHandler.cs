using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Extensions.Options;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Queries.DataStorage.GetBlobsList
{
    public class GetBlobsListQueryHandler : IRequestHandler<GetBlobsListQuery, IEnumerable<string>>
    {
        private readonly AzureBlobStorageSettings _blobOptions;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public GetBlobsListQueryHandler(BlobServiceClient blobServiceClient, IOptions<AzureBlobStorageSettings> blobOptions)
        {
            _blobOptions = blobOptions.Value;
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(_blobOptions.ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task<IEnumerable<string>> Handle(GetBlobsListQuery request, CancellationToken cancellationToken)
        {
            var items = new List<string>();

            await foreach (var blobItem in _containerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items;
        }
    }
}

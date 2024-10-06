using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Extensions.Options;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Queries.DataStorage.GetBlob
{
    public class GetBlobQueryHandler : IRequestHandler<GetBlobQuery, byte[]>
    {
        private readonly AzureBlobStorageSettings _blobOptions;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public GetBlobQueryHandler(BlobServiceClient blobServiceClient, IOptions<AzureBlobStorageSettings> blobOptions)
        {
            _blobOptions = blobOptions.Value;
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(_blobOptions.ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task<byte[]> Handle(GetBlobQuery query, CancellationToken cancellationToken)
        {
            var blobClient = _containerClient.GetBlobClient(query.Name);

            if (await blobClient.ExistsAsync())
            {
                MemoryStream memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream, cancellationToken);
                byte[] byteArray = memoryStream.ToArray();

                return byteArray;
            }
            else
            {
                return null;
            }
        }
    }
}

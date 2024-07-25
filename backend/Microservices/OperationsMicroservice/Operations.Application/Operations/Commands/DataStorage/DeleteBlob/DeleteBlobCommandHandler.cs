using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Extensions.Options;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Commands.DataStorage.DeleteBlob
{
    public class DeleteBlobCommandHandler : IRequestHandler<DeleteBlobCommand>
    {
        private readonly AzureBlobStorageSettings _blobOptions;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public DeleteBlobCommandHandler(BlobServiceClient blobServiceClient, IOptions<AzureBlobStorageSettings> blobOptions)
        {
            _blobOptions = blobOptions.Value;
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(_blobOptions.ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task Handle(DeleteBlobCommand command, CancellationToken cancellationToken)
        {
            var blobClient = _containerClient.GetBlobClient(command.BlobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Operations.Application.Extensions;
using Operations.Application.Settings;
using SharpCompress.Common;
using System.IO;

namespace Operations.Application.Operations.Commands.DataStorage.UploadFileBlob
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileBlobCommand>
    {
        private readonly AzureBlobStorageSettings _blobOptions;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public UploadFileCommandHandler(BlobServiceClient blobServiceClient, IOptions<AzureBlobStorageSettings> blobOptions)
        {
            _blobOptions = blobOptions.Value;
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(_blobOptions.ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task Handle(UploadFileBlobCommand command, CancellationToken cancellationToken)
        {
            var blobClient = _containerClient.GetBlobClient(command.Model.FileName);
            blobClient.Upload(new MemoryStream(command.Model.FileBytes), overwrite: true);
        }
    }
}

using MediatR;

namespace Operations.Application.Operations.Commands.DataStorage.DeleteBlob
{
    public class DeleteBlobCommand : IRequest
    {
        public string BlobName {  get; set; }

        public DeleteBlobCommand(string blobName)
        {
            BlobName = blobName;
        }
    }
}

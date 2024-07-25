using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.DataStorage.UploadFileBlob
{
    public class UploadFileBlobCommand : IRequest
    {
        public UploadFileRequestModel Model { get; set; }

        public UploadFileBlobCommand(UploadFileRequestModel model) 
        {
            Model = model;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Operations.Commands.DataStorage.DeleteBlob;
using Operations.Application.Operations.Commands.DataStorage.UploadFileBlob;
using Operations.Application.Operations.Queries.DataStorage.GetBlob;
using Operations.Application.Operations.Queries.DataStorage.GetBlobsList;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/blob-storage")]
    public class BlobStorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlobStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlobAsync(string blobName)
        {
            var file = await _mediator.Send(new GetBlobQuery(blobName));

            return Ok(file);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetListBlobsAsync()
        {
            return Ok(await _mediator.Send(new GetBlobsListQuery()));
        }

        [HttpPost("file")]
        public async Task<IActionResult> UploadFileAsync([FromBody] UploadFileRequestModel model)
        {
            await _mediator.Send(new UploadFileBlobCommand(model));

            return Ok();
        }

        [HttpDelete("{blobName}")]
        public async Task<IActionResult> DeleteFileAsync(string blobName)
        {
            await _mediator.Send(new DeleteBlobCommand(blobName));

            return NoContent();
        }
    }
}

using MediatR;

namespace Operations.Application.Operations.Queries.DataStorage.GetBlob
{
    public class GetBlobQuery : IRequest<byte[]>
    {
        public string Name { get; set; }

        public GetBlobQuery(string name) 
        {
            Name = name;
        }
    }
}

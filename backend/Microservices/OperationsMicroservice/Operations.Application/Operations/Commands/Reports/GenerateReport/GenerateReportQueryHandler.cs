using AutoMapper;
using iText.Kernel.Pdf;
using iText.Layout;
using MediatR;
using Operations.Application.Extensions;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Commands.Reports.GenerateReport
{
    public class GenerateReportQueryHandler : IRequestHandler<GenerateReportQuery, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenerateReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<byte[]> Handle(GenerateReportQuery command, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            document.ApplyDocHeaderContentAndStyle();

            var categoryCount = await _unitOfWork.Categories.GetRecordsCountAsync();
            var paginationSettings = new PaginationSettings()
            {
                PageSize = (int)categoryCount
            };
            var categories = await _unitOfWork.Categories.GetAllAsync(paginationSettings, cancellationToken);
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);  
            
            if(command.Models.Count > 0)
            {
                foreach (var model in command.Models)
                {
                    document.ApplyDocContentAndStyle(model, categoryModels);
                }
            }
            else
            {
                document.ApplyNoOperationsContentAndStyle();
            }

            document.Close();
            await Task.Yield();

            byte[] pdfBytes = memoryStream.ToArray();

            return pdfBytes;
        }
    }
}
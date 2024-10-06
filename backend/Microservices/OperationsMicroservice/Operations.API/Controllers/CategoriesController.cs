using MediatR;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Operations.Queries.Details.GetCategoryDetails;
using Operations.Application.Operations.Queries.Lists.GetCategoryList;
using Operations.Application.Operations.Queries.RecordsCount.GetCategoryRecordsCount;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var categories = await _mediator.Send(new GetCategoryListQuery(paginationSettings));

            return Ok(categories);
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetRecordsCountAsync()
        {
            return Ok(await _mediator.Send(new GetCategoryRecordsCountQuery()));    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetByIdAsync(int id)
        {
            var category = await _mediator.Send(new GetCategoryDetailsQuery(id));

            return Ok(category);
        }
    }
}

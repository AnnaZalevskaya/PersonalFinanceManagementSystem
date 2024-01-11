using MediatR;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Operations.Queries.GetCategoryTypeDetails;
using Operations.Application.Operations.Queries.GetCategoryTypeList;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/category-types")]
    public class CategoryTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync([FromQuery] PaginationSettings paginationSettings)
        {
            var types = await _mediator.Send(new GetCategoryTypeListQuery(paginationSettings));

            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryTypeModel>> GetByIdAsync(int id)
        {
            var type = await _mediator.Send(new GetCategoryTypeDetailsQuery(id));

            return Ok(type);
        }
    }
}

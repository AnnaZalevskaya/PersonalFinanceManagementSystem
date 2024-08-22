﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Operations.Queries.Details.GetCategoryTypeDetails;
using Operations.Application.Operations.Queries.Lists.GetCategoryTypeList;
using Operations.Application.Operations.Queries.RecordsCount.GetCategoryTypeRecordsCount;
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

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetRecordsCountAsync()
        {
            return Ok(await _mediator.Send(new GetCategoryTypeRecordsCountQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryTypeModel>> GetByIdAsync(int id)
        {
            var type = await _mediator.Send(new GetCategoryTypeDetailsQuery(id));

            return Ok(type);
        }
    }
}

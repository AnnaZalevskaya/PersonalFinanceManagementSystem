﻿using Operations.Application.Interfaces;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;
using Operations.Infrastructure.Repositories.Base;

namespace Operations.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OperationsDbContext context) : base(context)
        {

        }
    }
}

using Operations.Application.Interfaces;
using Operations.Core.Entities;
using Operations.Infrastructure.Data;
using Operations.Infrastructure.Repositories.Base;

namespace Operations.Infrastructure.Repositories
{
    public class CategoryTypeRepository : BaseRepository<CategoryType>, ICategoryTypeRepository
    {
        public CategoryTypeRepository(OperationsDbContext context) : base(context)
        {

        }
    }
}

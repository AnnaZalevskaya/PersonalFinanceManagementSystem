using Auth.Application.Settings;

namespace Auth.UnitTests.Extensions.Builders
{
    public static class PaginationSettingsBuilder
    {
        public static PaginationSettings BuildPaginationSettings(int pageNumber, int pageSize)
        {
            return new PaginationSettings
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}

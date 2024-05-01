using Accounts.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Presentation.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<FinancialAccountsDbContext>())
                {
                    appContext.Database.Migrate();
                }
            }

            return host;
        }
    }
}

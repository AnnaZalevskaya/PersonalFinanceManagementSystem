﻿using Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>())
                {
                    appContext.Database.Migrate();
                }
            }

            return host;
        }
    }
}

using Accounts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Accounts.DataAccess.Data
{
    public class FinancialAccountsDbContext : DbContext
    {
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<FinancialAccountType> FinancialAccountTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public FinancialAccountsDbContext(DbContextOptions<FinancialAccountsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}

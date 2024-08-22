using Accounts.DataAccess.Entities;
using Accounts.DataAccess.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Accounts.DataAccess.Data
{
    public class FinancialAccountsDbContext : DbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<FinancialAccountType> FinancialAccountTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }
        public DbSet<FinancialGoalType> FinancialGoalTypes { get; set; }

        public FinancialAccountsDbContext(DbContextOptions<FinancialAccountsDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FinancialAccountConfiguration());
            builder.ApplyConfiguration(new FinancialAccountTypeConfiguration());
            builder.ApplyConfiguration(new CurrencyConfiguration());
            builder.ApplyConfiguration(new FinancialGoalConfiguration());
            builder.ApplyConfiguration(new FinancialGoalTypeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}

using Accounts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Accounts.DataAccess.EntityTypeConfigurations
{
    public class FinancialGoalTypeConfiguration : IEntityTypeConfiguration<FinancialGoalType>
    {
        public void Configure(EntityTypeBuilder<FinancialGoalType> builder)
        {
            builder.ToTable("financial_goal_type", "accounts");

            builder.Property(goal => goal.Id).HasColumnName("id");
            builder.HasKey(goal => goal.Id);
            builder.HasIndex(goal => goal.Id);

            builder.Property(goal => goal.Name).HasColumnName("name");
        }
    }
}

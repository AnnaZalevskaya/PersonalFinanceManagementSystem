using Accounts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.EntityTypeConfigurations
{
    public class FinancialGoalConfiguration : IEntityTypeConfiguration<FinancialGoal>
    {
        public void Configure(EntityTypeBuilder<FinancialGoal> builder)
        {
            builder.ToTable("financial_goal", "accounts");

            builder.Property(goal => goal.Id).HasColumnName("id");
            builder.HasKey(goal => goal.Id);
            builder.HasIndex(goal => goal.Id);

            builder.Property(goal => goal.Name).HasColumnName("name");
            builder.Property(goal => goal.AccountId).HasColumnName("account_id");
            builder.Property(goal => goal.TypeId).HasColumnName("type_id");
            builder.Property(goal => goal.Amount).HasColumnName("monetary_goal");
            builder.Property(goal => goal.StartDate).HasColumnName("start_date");
            builder.Property(goal => goal.EndDate).HasColumnName("end_date");

            builder.HasOne(goal => goal.Type).WithMany(type => type.FinancialGoals)
                .HasForeignKey(goal => goal.TypeId)
                .HasConstraintName("financial_goal_type_id_fkey")
                .IsRequired();
        }
    }
}

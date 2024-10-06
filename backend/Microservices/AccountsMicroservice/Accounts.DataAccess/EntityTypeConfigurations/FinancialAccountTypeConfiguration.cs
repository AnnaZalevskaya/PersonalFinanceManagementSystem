using Accounts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.EntityTypeConfigurations
{
    public class FinancialAccountTypeConfiguration : IEntityTypeConfiguration<FinancialAccountType>
    {
        public void Configure(EntityTypeBuilder<FinancialAccountType> builder)
        {
            builder.ToTable("financial_account_type", "accounts");

            builder.Property(type => type.Id).HasColumnName("id");
            builder.HasKey(type => type.Id);
            builder.HasIndex(type => type.Id);

            builder.Property(type => type.Name).HasColumnName("name");
        }
    }
}

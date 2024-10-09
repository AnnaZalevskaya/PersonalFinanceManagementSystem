using Accounts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.EntityTypeConfigurations
{
    public class FinancialAccountConfiguration : IEntityTypeConfiguration<FinancialAccount>
    {
        public void Configure(EntityTypeBuilder<FinancialAccount> builder)
        {
            builder.ToTable("financial_account", "accounts");

            builder.Property(account => account.Id).HasColumnName("id");
            builder.HasKey(account => account.Id);
            builder.HasIndex(account => account.Id);

            builder.Property(account => account.Name).HasColumnName("name");
            builder.Property(account => account.AccountTypeId).HasColumnName("financial_account_type_id");
            builder.Property(account => account.CurrencyId).HasColumnName("currency_id");
            builder.Property(account => account.UserId).HasColumnName("user_id");

            builder.HasOne(account => account.AccountType).WithMany(type => type.FinancialAccounts)
                .HasForeignKey(account => account.AccountTypeId)
                .HasConstraintName("account_account_type_id_fkey")
                .IsRequired();

            builder.HasOne(account => account.Currency).WithMany(currency => currency.FinancialAccounts)
                .HasForeignKey(account => account.CurrencyId)
                .HasConstraintName("financial_account_currency_id_fkey")
                .IsRequired();
        }
    }
}

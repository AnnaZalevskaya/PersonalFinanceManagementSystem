using Accounts.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.DataAccess.EntityTypeConfigurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("currency", "accounts");

            builder.Property(currency => currency.Id).HasColumnName("id");
            builder.HasKey(currency => currency.Id);
            builder.HasIndex(currency => currency.Id);

            builder.Property(currency => currency.Name).HasColumnName("name");
            builder.Property(currency => currency.Abbreviation).HasColumnName("abbreviation");
            builder.Property(currency => currency.Sign).HasColumnName("sign");
        }
    }
}

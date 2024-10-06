using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.EntityTypeConfigurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
        {
            builder.ToTable("UserLogins");
            builder.HasKey("UserId");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.User.Admin;

namespace ScoreZone.Infrastructure.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<AdminEntity>
    {
        public void Configure(EntityTypeBuilder<AdminEntity> builder)
        {
            builder.ToTable("Admins");
            builder.HasKey(x => x.Id);

        }
    }
}
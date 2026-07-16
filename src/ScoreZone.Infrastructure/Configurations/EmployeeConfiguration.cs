using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScoreZone.Domain.User.Employee;

namespace ScoreZone.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);

        }
    }
}
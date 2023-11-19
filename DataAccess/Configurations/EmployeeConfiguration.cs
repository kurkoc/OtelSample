using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OtelSample;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.HasKey(q=>q.Id);

        builder.Property(q=>q.Id)
            .ValueGeneratedOnAdd();

        builder.Property(q=>q.FirstName)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(q=>q.LastName)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(q=>q.UserName)
        .IsRequired()
        .HasMaxLength(50);
    }
}

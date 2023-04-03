using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Sql.Configurations;

public class ProvinceEntityTypeConfiguration
    : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable("Provinces");
        builder.HasKey(cr => cr.Id);
        builder.Property(cr => cr.Name).IsRequired();
    }
}

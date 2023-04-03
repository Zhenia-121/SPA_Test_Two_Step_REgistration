using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Sql.Configurations;

public class CountryEntityTypeConfiguration
    : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");
        builder.HasKey(cr => cr.Id);
        builder.Property(cr => cr.Name).IsRequired();
        builder.Property(cr => cr.Code).IsRequired();

        builder.HasMany(c => c.Provinces)
            .WithOne(p => p.Country)
            .HasForeignKey(p => p.CountryId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

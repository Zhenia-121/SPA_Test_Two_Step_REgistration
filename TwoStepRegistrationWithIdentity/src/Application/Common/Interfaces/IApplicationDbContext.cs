using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Country> Countries { get; }

    DbSet<Domain.Entities.Province> Provinces { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

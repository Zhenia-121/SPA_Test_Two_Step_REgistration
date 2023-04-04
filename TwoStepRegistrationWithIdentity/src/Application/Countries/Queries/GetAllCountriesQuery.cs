using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Countries.Queries;

public record GetAllCountriesQuery : IRequest<List<CountryListItemDto>>
{
}

public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, List<CountryListItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllCountriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CountryListItemDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Countries
            .AsNoTracking()
            .Select(c => new CountryListItemDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code
            })
            .ToListAsync(cancellationToken);
    }
}

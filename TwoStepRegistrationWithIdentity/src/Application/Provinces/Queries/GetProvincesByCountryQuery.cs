using Application.Common.Interfaces;
using Application.Province.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Provinces.Queries
{
    public record GetProvincesByCountryQuery : IRequest<List<ProvinceListItemDto>>
    {
        public int CountryId { get; set; }
    }

    public class GetProvincesByCountryQueryHandler : IRequestHandler<GetProvincesByCountryQuery, List<ProvinceListItemDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetProvincesByCountryQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProvinceListItemDto>> Handle(GetProvincesByCountryQuery request, CancellationToken cancellationToken)
        {
            return await _context
                .Provinces
                .AsNoTracking()
                .Where(p => p.CountryId == request.CountryId)
                .Select(c => new ProvinceListItemDto
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync(cancellationToken);
        }
    }
}

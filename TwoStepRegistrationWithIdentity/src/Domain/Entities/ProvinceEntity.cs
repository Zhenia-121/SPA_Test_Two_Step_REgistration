using Domain.Common;

namespace Domain.Entities
{
    public class Province : BaseAuditableEntity
    {
        public string? Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; } = null!;
    }
}

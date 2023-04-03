using Domain.Common;

namespace Domain.Entities
{
    public class Country : BaseAuditableEntity
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public List<Province> Provinces { get; private set; } = new();
    }
}

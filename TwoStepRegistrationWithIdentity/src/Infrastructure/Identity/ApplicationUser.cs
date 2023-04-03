using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public bool ServiceAgreementAccepted { get; set; }
    public int? ProvinceId { get; set; }
}

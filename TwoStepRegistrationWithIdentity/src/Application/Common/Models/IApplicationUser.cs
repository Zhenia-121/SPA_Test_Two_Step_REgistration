namespace Application.Common.Models
{
    public class RegisterUser
    {
        string? Email { get; set; }

        string? Password { get; set; }

        string? ConfirmedPassword { get; set; }

        bool IsAgreementAccepted { get; set; }

        int? ProvinceId { get; set; }
    }
}

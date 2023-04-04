using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands.Register;
public record RegisterUserCommand : IRequest<string>
{
    public string? Email { get; set; }

    public string Password { get; set; }

    public string ConfirmedPassword { get; set; }

    public bool IsAgreementAccepted { get; set; }

    public int? ProvinceId { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var (result, userId) = await _identityService.RegisterAsync(request);

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.ToList());
        }

        return userId;
    }
}


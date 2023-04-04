using System.Text.RegularExpressions;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.Users.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IOptions<PasswordRequirements> _passwordRequirements;

    public RegisterUserCommandValidator(
        IApplicationDbContext context, 
        IIdentityService identityService,
        IOptions<PasswordRequirements> passwordRequirements)
    {
        _context = context;
        _identityService = identityService;
        _passwordRequirements = passwordRequirements;

        RuleFor(c => c.Password)
            .Matches(c => c.ConfirmedPassword)
            .WithMessage("Passwords should be equal")
            .Must(pass => Regex.IsMatch(pass, _passwordRequirements?.Value?.Regex))
            .WithMessage("Password should contain at least one number and one symbol");

        RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("Email is invalid.")
            .MustAsync(ShouldBeUnique).WithMessage("Email is already taken");

        RuleFor(c => c.ProvinceId)
            .MustAsync(DoesProvinceExist)
            .WithMessage(c => $"Province with id {c.ProvinceId} doesn't exist");

        RuleFor(c => c.IsAgreementAccepted)
            .Must(c => c)
            .WithMessage("Service agreement should be accepted");
    }

    public async Task<bool> DoesProvinceExist(int? province, CancellationToken cancellationToken)
    {
        return await _context.Provinces.AnyAsync(p => p.Id == province, cancellationToken);
    }

    public async Task<bool> ShouldBeUnique(string email, CancellationToken cancellationToken)
    {
        return !await _identityService.IsUserLoginTakenAsync(email);
    }
}

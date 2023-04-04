using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Commands.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public Task<bool> IsUserLoginTakenAsync(string login)
    {
        return _userManager.Users.AnyAsync(u => u.Email == login);
    }

    public async Task<(Result Result, string UserId)> RegisterAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<(Result Result, string UserId)> RegisterAsync(RegisterUserCommand user)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            ServiceAgreementAccepted = user.IsAgreementAccepted,
            ProvinceId = user.ProvinceId
        };

        var result = await _userManager.CreateAsync(applicationUser, user.Password);

        return (result.ToApplicationResult(), applicationUser.Id);
    }
}

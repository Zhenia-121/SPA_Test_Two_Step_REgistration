using Application.Users.Commands.Register;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsUserLoginTakenAsync(string login);

    Task<(Result Result, string UserId)> RegisterAsync(string userName, string password);

    Task<(Result Result, string UserId)> RegisterAsync(RegisterUserCommand user);
}
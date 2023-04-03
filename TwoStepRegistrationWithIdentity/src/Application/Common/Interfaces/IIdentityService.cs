public interface IIdentityService
{
    Task<bool> IsUserLoginTakenAsync(string login);

    Task<(Result Result, string UserId)> RegisterAsync(string userName, string password);
}

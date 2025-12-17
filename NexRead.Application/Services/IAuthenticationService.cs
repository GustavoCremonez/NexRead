namespace NexRead.Application.Services;

public interface IAuthenticationService
{
    Task SignInAsync(Guid userId, string userName, string userEmail, CancellationToken cancellationToken = default);
    Task SignOutAsync(CancellationToken cancellationToken = default);
    Guid? GetCurrentUserId();
}

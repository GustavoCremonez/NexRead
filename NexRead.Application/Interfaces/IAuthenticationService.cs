namespace NexRead.Application.Interfaces;

public interface IAuthenticationService
{
    Task SignInAsync(Guid userId, string userName, string userEmail, CancellationToken cancellationToken = default);
    Task SignOutAsync(CancellationToken cancellationToken = default);
    Guid? GetCurrentUserId();
}

using DN.WebApi.Application.Common;
using System.Security.Claims;

namespace DN.WebApi.Application.Identity.Users;

public interface ICurrentUser : IScopedService
{
    string? Name { get; }

    Guid GetUserId();

    string? GetUserEmail();

    string? GetTenant();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();

    void SetUser(ClaimsPrincipal user);

    void SetUserJob(string userId);
}
﻿using FSH.Framework.Core.Exceptions;
using System.Security.Claims;
using FSH.Framework.Core.Identity.Users.Abstractions;
using FSH.Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FSH.Framework.Infrastructure.Identity.Users.Endpoints;
public static class GetMeEndpoint
{
    internal static RouteHandlerBuilder MapGetMeEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/me", async (ClaimsPrincipal user, IUserService service, CancellationToken cancellationToken) =>
        {
            if (user.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException();
            }
            return service.GetAsync(userId, cancellationToken);
        })
        .WithName("GetMeEndpoint")
        .WithSummary("Get current user information based on token")
        .RequirePermission("Permissions.Users.View")
        .WithDescription("Get current user information based on token");
    }
}

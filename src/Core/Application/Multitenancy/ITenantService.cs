﻿namespace FSH.WebApi.Application.Multitenancy;

public interface ITenantService
{
    Task<List<TenantDto>> GetAllAsync();
    Task<bool> ExistsWithIdAsync(string id);
    Task<TenantDto> GetByIdAsync(string id);
    Task<string> CreateAsync(string id, string name, string? connectionString, string adminEmail, CancellationToken cancellationToken);
    Task<string> ActivateAsync(string id);
    Task<string> DeactivateAsync(string id);
    Task<string> UpdateSubscription(string id, DateTime extendedExpiryDate);
}
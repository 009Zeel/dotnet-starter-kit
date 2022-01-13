﻿using Finbuckle.MultiTenant;
using FSH.WebApi.Shared.Multitenancy;

namespace FSH.WebApi.Infrastructure.Multitenancy;

public class FSHTenantInfo : ITenantInfo
{
    public FSHTenantInfo()
    {
    }

    public FSHTenantInfo(string id, string name, string connectionString, string adminEmail)
    {
        Id = id;
        Identifier = id;
        Name = name;
        ConnectionString = connectionString;
        AdminEmail = adminEmail;
        IsActive = true;

        // Add Default 1 Month Validity for all new tenants. Something like a DEMO period for tenants.
        ValidUpto = DateTime.UtcNow.AddMonths(1);
    }

    public string Id { get; set; } = default!;
    public string Identifier { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string ConnectionString { get; set; } = default!;

    public string AdminEmail { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public DateTime ValidUpto { get; private set; }

    public string? Issuer { get; set; }

    public void AddValidity(int months) =>
        ValidUpto = ValidUpto.AddMonths(months);

    public void SetValidity(in DateTime validTill) =>
        ValidUpto = ValidUpto < validTill
            ? validTill
            : throw new Exception("Subscription cannot be backdated.");

    public void Activate()
    {
        if (Identifier == MultitenancyConstants.Root.Id)
        {
            throw new InvalidOperationException("Invalid Tenant");
        }

        IsActive = true;
    }

    public void Deactivate()
    {
        if (Identifier == MultitenancyConstants.Root.Id)
        {
            throw new InvalidOperationException("Invalid Tenant");
        }

        IsActive = false;
    }
}
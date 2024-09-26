using FSH.Framework.Core.Paging;
using FSH.Framework.Infrastructure.Auth.Policy;
using FSH.Starter.WebApi.Catalog.Application.Products.Export.v1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;

public static class ExportProductsEndpoint
{
    internal static RouteHandlerBuilder MapExportProductsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/export", async Task<FileStreamHttpResult> (ISender mediator, [FromBody] BaseFilter filter) =>
            {
                var fileStream = await mediator.Send(new ExportProductsCommand(filter));
                var response = TypedResults.File(fileStream, "application/octet-stream", "ProductExports");
                return response;
                
               //return TypedResults.File(fileStream, "application/octet-stream", "ProductExports");
               //return TypedResults.File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductExports.xlsx");
            })
            .WithName(nameof(ExportProductsEndpoint))
            .WithSummary("Exports a list of products")
            .WithDescription("Exports a list of products with filtering support")
            .Produces <FileStreamHttpResult>()
            .RequirePermission("Permissions.Products.Export")
            .MapToApiVersion(1);
    }
}


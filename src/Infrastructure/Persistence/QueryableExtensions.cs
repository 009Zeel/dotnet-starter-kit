using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using DN.WebApi.Application.Common.Specifications;
using DN.WebApi.Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DN.WebApi.Infrastructure.Persistence;

public static class QueryableExtensions
{
    public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> specification)
    where T : class, IEntity
    {
        /*var queryableResultWithIncludes = spec.Includes
            .Aggregate(query, (current, include) => current.Include(include));
        var secondaryResult = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));
        if (spec.Criteria == null)
            return secondaryResult;
        else
            return secondaryResult.Where(spec.Criteria);*/

        if (specification == null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        if (specification.Conditions?.Any() == true)
        {
            foreach (var specificationCondition in specification.Conditions)
            {
                query = query.Where(specificationCondition);
            }
        }

        if (specification.Includes != null)
        {
            query = query.IncludeMultiple(specification.Includes);
        }

        if (specification.OrderByStrings?.Length > 0)
        {
            return query.ApplySort(specification.OrderByStrings);
        }
        else if (specification.OrderBy != null)
        {
            return specification.OrderBy(query);
        }

        return query;
    }

    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Filters<T>? filters)
    {
        if (filters?.IsValid() == true)
            query = filters.Get().Aggregate(query, (current, filter) => current.Where(filter.Expression));
        return query;
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string[]? orderBy)
    where T : class, IEntity
    {
        string? ordering = new OrderByConverter().ConvertBack(orderBy);
        return !string.IsNullOrWhiteSpace(ordering) ? query.OrderBy(ordering) : query.OrderBy("Id");
    }

    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
    where T : class, IEntity
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include.AsPath()));
        }

        return query;
    }
}
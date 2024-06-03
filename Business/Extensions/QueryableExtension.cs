using Microsoft.EntityFrameworkCore;

namespace Business.Extensions;

public static class QueryableExtension
{
    public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> queryable, int page, int pageSize)
    {
        var totalItems = queryable.Count();
        var currentPageItems = queryable.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<T>
        {
            Items = currentPageItems,
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalItems > 0 ? totalItems / pageSize : 0
        };
    }

    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> queryable, int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var totalItems = await queryable.CountAsync(cancellationToken);
        var currentPageItems =
            await queryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            Items = currentPageItems,
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = totalItems > 0 ? totalItems / pageSize : 0
        };
    }
}

public record PagedResult<T>
{
    public required IEnumerable<T> Items { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
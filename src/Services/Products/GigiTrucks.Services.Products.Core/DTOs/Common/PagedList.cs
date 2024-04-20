using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Products.Core.DTOs.Common;

public class PagedList<T>(List<T> items, int pageNumber, int pageSize, int totalCount)
{
    public List<T> Items { get; } = items;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
    public bool HasPreviousPage => PageNumber > 1;
}
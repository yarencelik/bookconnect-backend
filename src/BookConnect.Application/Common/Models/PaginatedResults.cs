namespace BookConnect.Application.Common.Models;

public record PaginatedResults<T>(IEnumerable<T> Results, PageMetadata PageData);
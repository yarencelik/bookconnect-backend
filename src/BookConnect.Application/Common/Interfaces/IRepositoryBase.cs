using System.Linq.Expressions;
using BookConnect.Application.Common.Models;

namespace BookConnect.Application.Common.Interfaces;

public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> GetAllValues();

    Task<IEnumerable<T>> GetAllValues(Expression<Func<T, bool>>? exp = null, List<Expression<Func<T, object>>>? includesExp = null, bool AsNoTracking = true);

    Task<(IEnumerable<T>, PageMetadata)> GetAllValuesPaginated(
        int page = 1, 
        int pageSize = 10,
        Expression<Func<T, bool>>? exp = null, 
        List<Expression<Func<T, object>>>? includeExp = null,
        bool AsNoTracking = true
        );

    Task<T?> GetValue(Expression<Func<T, bool>> exp, List<Expression<Func<T, object>>>? includes = null, bool AsNoTracking = true);

    Task Create(T entity);

    void Delete(T entity);

    Task SaveChangesAsync();
}

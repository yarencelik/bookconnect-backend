using BookConnect.Application.Common.Interfaces;
using BookConnect.Application.Common.Models;
using BookConnect.Domain.Common;
using BookConnect.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookConnect.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    protected RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllValues()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllValues(Expression<Func<T, bool>>? exp = null, List<Expression<Func<T, object>>>? includesExp = null, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if (AsNoTracking)
            query = query.AsNoTracking();

        if (exp != null)
            query = query.Where(exp);

        if (includesExp != null)
            query = includesExp.Aggregate(query, (current, include) => current.Include(include));

        return await query
            .ToListAsync();
    }
    public async Task<(IEnumerable<T>, PageMetadata)> GetAllValuesPaginated(int page = 1, int pageSize = 10, Expression<Func<T, bool>>? exp = null, List<Expression<Func<T, object>>>? includeExp = null, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if(AsNoTracking)
            query = query.AsNoTracking();
        
        if(includeExp != null)
            query = includeExp.Aggregate(query, (current, include) => current.Include(include));
        
        if(exp != null)
            query = query.Where(exp);

        var totalAuthorsCount = await query.CountAsync();

        var pageData = new PageMetadata(page, pageSize, totalAuthorsCount);
        
        var results = await query
            .OrderByDescending(x => x.UpdatedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Skip(pageSize * (page -1))
            .Take(pageSize)
            .ToListAsync();

        return (results, pageData);

    }

    public async Task<T?> GetValue(Expression<Func<T, bool>> exp, List<Expression<Func<T, object>>>? includes = null, bool AsNoTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();

        if (AsNoTracking)
            query = query.AsNoTracking();

        if(includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query
            .FirstOrDefaultAsync(exp);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

using System.Linq.Expressions;
using AltaHomework_For_23_Oct.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AltaHomework_For_23_Oct.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbSet<TEntity> dbSet) =>
        _dbSet = dbSet;

    public TEntity? GetFirstOfDefault(Expression<Func<TEntity, bool>> expression) =>
        _dbSet.FirstOrDefault(expression);

    public Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        query = includeProperties
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        if (orderBy != null)
            return orderBy(query).ToListAsync();

        return query.ToListAsync();
    }

    public void Create(TEntity item) =>
        _dbSet.Add(item);

    public void Update(TEntity item) =>
        _dbSet.Entry(item).State = EntityState.Modified;

    public void Delete(object id)
    {
        var entity = _dbSet.Find(id);
        if (entity == null)
            throw new RecordNotFoundException();
        _dbSet.Remove(entity);
    }
}

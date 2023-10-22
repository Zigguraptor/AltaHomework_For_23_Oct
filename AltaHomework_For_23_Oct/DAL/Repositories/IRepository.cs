using System.Linq.Expressions;

namespace AltaHomework_For_23_Oct.DAL.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    public TEntity? GetFirstOfDefault(Expression<Func<TEntity, bool>> expression);

    public Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    public void Create(TEntity item);
    public void Update(TEntity item);
    public void Delete(object id);
}

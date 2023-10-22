using Microsoft.EntityFrameworkCore;

namespace AltaHomework_For_23_Oct.DAL.Repositories;

public interface IEntityDb<TEntity> where TEntity : class
{
    public DbSet<TEntity> Entity { get; }
}

using AltaHomework_For_23_Oct.DAL.DbContexts;
using AltaHomework_For_23_Oct.DAL.Entities;
using AltaHomework_For_23_Oct.DAL.Repositories;

namespace AltaHomework_For_23_Oct.DAL;

public sealed class UnitOfWork : IDisposable, IAsyncDisposable
{
    private readonly UsersDataDbContext _dbContext;

    private Repository<UserEntity>? _usersRepository;
    private FriendshipRepository? _friendshipRepository;
    public UnitOfWork(UsersDataDbContext dbContext) => _dbContext = dbContext;
    public Repository<UserEntity> UsersRepository => _usersRepository ??= new Repository<UserEntity>(_dbContext.Users);
    public FriendshipRepository FriendshipRepository => _friendshipRepository ??=
        new FriendshipRepository(_dbContext);

    public async ValueTask DisposeAsync() =>
        await _dbContext.DisposeAsync();

    public void Dispose() => _dbContext.Dispose();

    public Task<int> SaveAsync() => _dbContext.SaveChangesAsync();
}

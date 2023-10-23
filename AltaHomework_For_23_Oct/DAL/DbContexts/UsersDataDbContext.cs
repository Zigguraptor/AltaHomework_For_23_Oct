using AltaHomework_For_23_Oct.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AltaHomework_For_23_Oct.DAL.DbContexts;

public class UsersDataDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<FriendshipEntity> UsersFriends { get; set; } = null!;
    public DbSet<FriendsRequestEntity> FriendsRequests { get; set; } = null!;
    public DbSet<MessageEntity> UserMessages { get; set; } = null!;

    public UsersDataDbContext(DbContextOptions options) : base(options) =>
        Init();

    public void Init() => Database.EnsureCreated();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MessageEntity>()
            .ToTable("Messages");

        modelBuilder.Entity<MessageEntity>()
            .HasKey(m => m.Guid);

        modelBuilder.Entity<MessageEntity>()
            .Property(e => e.MessageText)
            .HasColumnName("MessageText");

        modelBuilder.Entity<MessageEntity>()
            .HasOne(u => u.Sender)
            .WithMany(u => u.OutMessages)
            .HasForeignKey(m => m.SenderGuid);

        modelBuilder.Entity<MessageEntity>()
            .HasOne(u => u.Recipient)
            .WithMany(u => u.InMessages)
            .HasForeignKey(m => m.RecipientGuid);

        modelBuilder.Entity<MessageEntity>()
            .HasIndex(m => m.SenderGuid)
            .IsUnique(false);

        modelBuilder.Entity<MessageEntity>()
            .HasIndex(m => m.RecipientGuid)
            .IsUnique(false);

        modelBuilder.Entity<MessageEntity>()
            .Ignore(m => m.RandomProp);

        modelBuilder.Entity<MessageEntity>()
            .Property(m => m.MessageText)
            .IsRequired();
    }
}

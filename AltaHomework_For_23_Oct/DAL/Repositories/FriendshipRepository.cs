using AltaHomework_For_23_Oct.DAL.DbContexts;
using AltaHomework_For_23_Oct.DAL.Entities;
using AltaHomework_For_23_Oct.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AltaHomework_For_23_Oct.DAL.Repositories;

public class FriendshipRepository
{
    private readonly UsersDataDbContext _usersDataDbContext;

    public FriendshipRepository(UsersDataDbContext usersDataDbContext) =>
        _usersDataDbContext = usersDataDbContext;

    /// <returns>Errors</returns>
    public string? FriendsRequestAsync(FriendshipDto friendshipDto)
    {
        var requestEntity = _usersDataDbContext.FriendsRequests.FirstOrDefault(e =>
            e.User1Guid == friendshipDto.SubjectUserGuid && e.User2Guid == friendshipDto.ObjectUserGuid);

        if (requestEntity != null)
            return "Friend request already exist.\n";

        requestEntity = _usersDataDbContext.FriendsRequests.FirstOrDefault(e =>
            e.User2Guid == friendshipDto.SubjectUserGuid && e.User1Guid == friendshipDto.ObjectUserGuid);

        if (requestEntity != null)
        {
            var friendshipEntity = new FriendshipEntity
            {
                CreationDateTime = DateTime.UtcNow,
                LastModDateTime = DateTime.UtcNow,
                User1Guid = friendshipDto.SubjectUserGuid,
                User2Guid = friendshipDto.ObjectUserGuid
            };

            _usersDataDbContext.UsersFriends.Add(friendshipEntity);
            _usersDataDbContext.FriendsRequests.Remove(requestEntity);

            return null;
        }

        var friendshipRequestEntity = new FriendsRequestEntity
        {
            CreationDateTime = DateTime.UtcNow,
            LastModDateTime = DateTime.UtcNow,
            User1Guid = friendshipDto.ObjectUserGuid,
            User2Guid = friendshipDto.SubjectUserGuid
        };

        _usersDataDbContext.FriendsRequests.Add(friendshipRequestEntity);

        return null;
    }

    public Task<List<string>> GetFriendsByNameAsync(string userName)
    {
        return _usersDataDbContext.UsersFriends
            .Where(e => e.User1.UserName.ToLower() == userName.ToLower()).Select(e => e.User2.UserLink)
            .Union(
                _usersDataDbContext.UsersFriends
                    .Where(e => e.User2.UserName.ToLower() == userName.ToLower()).Select(e => e.User1.UserLink))
            .ToListAsync();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AltaHomework_For_23_Oct.Common.Entities;

namespace AltaHomework_For_23_Oct.DAL.Entities;

public abstract class BaseFriendshipEntity : CommonCredentials
{
    [Key] public Guid User1Guid { get; set; }
    public Guid User2Guid { get; set; }
    [Required] [ForeignKey("User1Guid")] public UserEntity User1 { get; set; } = null!;
    [Required] [ForeignKey("User2Guid")] public UserEntity User2 { get; set; } = null!;
}

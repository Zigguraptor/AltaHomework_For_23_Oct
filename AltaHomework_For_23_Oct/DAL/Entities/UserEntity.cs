using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AltaHomework_For_23_Oct.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace AltaHomework_For_23_Oct.DAL.Entities;

[Table("Users")]
[Index(nameof(UserName), IsUnique = true)]
public class UserEntity : CommonCredentials
{
    [Key] public Guid Guid { get; set; }
    [Required] public bool IsActive { get; set; } = true;
    [Required] public required string UserName { get; set; }
    [Required] public required string DisplayName { get; set; }
    public ICollection<MessageEntity> InMessages { get; set; } = null!;
    public ICollection<MessageEntity> OutMessages { get; set; } = null!;

    [NotMapped] public string UserLink => '@' + UserName;
}

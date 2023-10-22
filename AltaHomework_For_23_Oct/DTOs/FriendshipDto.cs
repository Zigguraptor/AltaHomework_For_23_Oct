using System.ComponentModel.DataAnnotations;

namespace AltaHomework_For_23_Oct.DTOs;

public class FriendshipDto
{
    [Required] public Guid SubjectUserGuid { get; set; }
    [Required] public Guid ObjectUserGuid { get; set; }
}

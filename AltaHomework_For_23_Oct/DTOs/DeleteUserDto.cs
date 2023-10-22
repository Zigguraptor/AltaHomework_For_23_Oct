using System.ComponentModel.DataAnnotations;

namespace AltaHomework_For_23_Oct.DTOs;

public class DeleteUserDto
{
    [Required] public Guid Guid { get; set; }
}

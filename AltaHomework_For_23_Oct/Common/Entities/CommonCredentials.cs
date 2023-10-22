using System.ComponentModel.DataAnnotations;

namespace AltaHomework_For_23_Oct.Common.Entities;

public class CommonCredentials
{
    [Required] public DateTime CreationDateTime { get; set; } = DateTime.Now;
    [Required] public DateTime LastModDateTime { get; set; } = DateTime.Now;
}

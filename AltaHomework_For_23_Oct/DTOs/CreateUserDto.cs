using System.ComponentModel.DataAnnotations;
using AltaHomework_For_23_Oct.Common.Mappings;
using AltaHomework_For_23_Oct.DAL.Entities;
using AutoMapper;

namespace AltaHomework_For_23_Oct.DTOs;

public class CreateUserDto : IMappingSource
{
    [Required(AllowEmptyStrings = false)]
    [MinLength(3)]
    public required string UserName { get; set; }

    [Required(AllowEmptyStrings = false)]
    [MinLength(1)]
    public required string DisplayName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserDto, UserEntity>();
    }
}

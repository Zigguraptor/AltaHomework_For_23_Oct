using System.ComponentModel.DataAnnotations;
using AltaHomework_For_23_Oct.Common.Mappings;
using AltaHomework_For_23_Oct.DAL.Entities;
using AutoMapper;

namespace AltaHomework_For_23_Oct.DTOs;

public class UpdateUserDto : IMappingSource
{
    [Required] public Guid Guid { get; set; }
    [Required] [MinLength(3)] public required string UserName { get; set; }
    [Required] [MinLength(1)] public required string DisplayName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserDto, UserEntity>()
            .ForMember(entity => entity.UserName, o =>
                o.MapFrom(dto => dto.UserName))
            .ForMember(entity => entity.DisplayName, o =>
                o.MapFrom(dto => dto.DisplayName));
    }
}

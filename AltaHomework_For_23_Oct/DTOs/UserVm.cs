using AltaHomework_For_23_Oct.Common.Mappings;
using AltaHomework_For_23_Oct.DAL.Entities;
using AutoMapper;

namespace AltaHomework_For_23_Oct.DTOs;

public class UserVm : IMappingSource
{
    public Guid Guid { get; set; }
    public required string UserName { get; set; }
    public required string DisplayName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserEntity, UserVm>();
    }
}

using System.ComponentModel.DataAnnotations;
using AltaHomework_For_23_Oct.Common.Mappings;
using AltaHomework_For_23_Oct.DAL.Entities;
using AutoMapper;

namespace AltaHomework_For_23_Oct.DTOs;

public class MessageDto : IMappingSource
{
    [Required] public Guid SenderGuid { get; set; }
    [Required] public Guid RecipientGuid { get; set; }
    [Required(AllowEmptyStrings = false)] public required string MessageText { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MessageDto, MessageEntity>();
    }
}

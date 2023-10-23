using AltaHomework_For_23_Oct.Common.Entities;
using AltaHomework_For_23_Oct.Common.Mappings;
using AltaHomework_For_23_Oct.DTOs;
using AutoMapper;

namespace AltaHomework_For_23_Oct.DAL.Entities;

public class MessageEntity : CommonCredentials, IMappingSource
{
    public Guid Guid { get; set; }
    public required string MessageText { get; set; }
    public Guid SenderGuid { get; set; }
    public Guid RecipientGuid { get; set; }
    public UserEntity Sender { get; set; } = null!;
    public UserEntity Recipient { get; set; } = null!;
    public string RandomProp => string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MessageEntity, MessageVm>()
            .ForMember(vm => vm.Sender, o =>
                o.MapFrom(e => e.Sender.UserLink))
            .ForMember(vm => vm.Recipient, o =>
                o.MapFrom(e => e.Recipient.UserLink))
            .ForMember(vm => vm.MessageText, o =>
                o.MapFrom(e => e.MessageText));
    }
}

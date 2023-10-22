using AltaHomework_For_23_Oct.Common.Entities;

namespace AltaHomework_For_23_Oct.DAL.Entities;

public class MessageEntity : CommonCredentials
{
    public Guid Guid { get; set; }
    public required string MessageText { get; set; }
    public Guid SenderGuid { get; set; }
    public Guid RecipientGuid { get; set; }
    public UserEntity Sender { get; set; } = null!;
    public UserEntity Recipient { get; set; } = null!;
    public string RandomProp => string.Empty;
}

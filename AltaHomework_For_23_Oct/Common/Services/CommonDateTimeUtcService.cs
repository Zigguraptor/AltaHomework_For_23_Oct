namespace AltaHomework_For_23_Oct.Common.Services;

public class CommonDateTimeUtcService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}
